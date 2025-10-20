# PowerShell post-commit hook: auto push current branch to origin
param()
try {
    $branch = git rev-parse --abbrev-ref HEAD
    if ($branch -eq 'HEAD') {
        Write-Host 'Detached HEAD; skipping auto-push.'
        exit 0
    }
    Write-Host "Auto-pushing branch $branch to origin..."
    & git push origin $branch --follow-tags
    if ($LASTEXITCODE -eq 0) {
        Write-Host 'Auto-push succeeded.'
        exit 0
    } else {
        Write-Host "Auto-push failed with exit code $LASTEXITCODE"
        exit $LASTEXITCODE
    }
} catch {
    Write-Host "post-commit hook failed: $_"
    exit 1
}
