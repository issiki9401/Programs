<#
Auto Commit & Push Script (PowerShell)

Usage:
  1) Put a file named .auto_git_enable at the repository root to enable the script.
     (This prevents accidental activation.)
  2) From repo root run:
     pwsh -NoProfile -ExecutionPolicy Bypass -File .\tools\auto_commit_and_push.ps1
     or run it in a background terminal / scheduled task.

Behavior:
  - Polls the repository every N seconds (default 10).
  - If there are tracked-file changes (git status --porcelain), it will `git add -A`,
    `git commit -m "Auto: <timestamp>"` and `git push origin <current-branch>`.
  - Skips operation when:
    - repository is in detached HEAD
    - a merge/rebase/cherry-pick/BISECT is in progress
    - `.auto_git_enable` is missing

Safety:
  - Require the presence of `.auto_git_enable` to run (prevent accidental start).
  - Recommend using on personal repos only. Do not run on protected/shared repos unless you intend to auto-commit everything.
  - Ensure credentials (credential helper / SSH key) are configured for non-interactive push.
#>

param(
    [int]$PollIntervalSeconds = 10,
    [string]$EnableFile = '.auto_git_enable',
    [switch]$VerboseMode
)

Set-StrictMode -Version Latest

$repoRoot = (Get-Location).Path
$enablePath = Join-Path $repoRoot $EnableFile

if (-not (Test-Path $enablePath)) {
    Write-Host "Auto-commit disabled: create the file '$EnableFile' at repo root to enable." -ForegroundColor Yellow
    exit 0
}

Write-Host "Auto-commit enabled (watching repo: $repoRoot). Poll interval: $PollIntervalSeconds s" -ForegroundColor Green

function InProgressOperation {
    param(
        [string]$gitDir = (Join-Path $repoRoot '.git')
    )
    return (Test-Path (Join-Path $gitDir 'MERGE_HEAD')) -or
           (Test-Path (Join-Path $gitDir 'REBASE_HEAD')) -or
           (Test-Path (Join-Path $gitDir 'CHERRY_PICK_HEAD')) -or
           (Test-Path (Join-Path $gitDir 'BISECT_LOG'))
}

while ($true) {
    try {
        # Ensure we are inside a git repo
        $isRepo = git rev-parse --is-inside-work-tree 2>$null
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Not a git repository (or git not in PATH). Exiting." -ForegroundColor Red
            exit 1
        }

        if (InProgressOperation) {
            Write-Host "Git operation in progress (merge/rebase/cherry-pick). Skipping this cycle." -ForegroundColor Yellow
            Start-Sleep -Seconds $PollIntervalSeconds
            continue
        }

        $branch = git rev-parse --abbrev-ref HEAD
        if ($branch -eq 'HEAD') {
            Write-Host "Detached HEAD detected; skipping auto commit/push." -ForegroundColor Yellow
            Start-Sleep -Seconds $PollIntervalSeconds
            continue
        }

        # Check for workspace changes (tracked or staged)
        $status = git status --porcelain
        if ([string]::IsNullOrWhiteSpace($status)) {
            if ($VerboseMode) { Write-Host "No changes detected." }
            Start-Sleep -Seconds $PollIntervalSeconds
            continue
        }

        Write-Host "Changes detected (status):" -ForegroundColor Cyan
        Write-Host $status

        # Stage all tracked changes and deletions
        git add -A
        # verify there is something to commit
        $postAdd = git status --porcelain
        if ([string]::IsNullOrWhiteSpace($postAdd)) {
            Write-Host "No changes to commit after staging (maybe only untracked files)." -ForegroundColor Yellow
            Start-Sleep -Seconds $PollIntervalSeconds
            continue
        }

        $timestamp = (Get-Date).ToString('yyyy-MM-dd HH:mm:ss')
        $commitMsg = "Auto: changes at $timestamp"
        git commit -m $commitMsg
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Commit failed (exit $LASTEXITCODE). See git output above." -ForegroundColor Red
            Start-Sleep -Seconds $PollIntervalSeconds
            continue
        }

        Write-Host "Committed: $commitMsg" -ForegroundColor Green

        # Push to origin
        Write-Host "Pushing branch $branch to origin..."
        git push origin $branch --follow-tags
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Push failed (exit $LASTEXITCODE). You'll need to resolve and push manually." -ForegroundColor Red
        } else {
            Write-Host "Auto-push succeeded." -ForegroundColor Green
        }

    } catch {
        Write-Host "Auto-commit loop error: $_" -ForegroundColor Red
    }

    Start-Sleep -Seconds $PollIntervalSeconds
}
