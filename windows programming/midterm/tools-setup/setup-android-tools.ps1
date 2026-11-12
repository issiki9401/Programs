$ErrorActionPreference = 'Continue'
$ProgressPreference = 'SilentlyContinue'

Write-Output "=== STEP 1: JDK 17 ==="
$jdk = Get-ChildItem "C:\Program Files\Microsoft" -Directory -Filter "jdk-17*" -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $jdk) {
    winget install -e --id Microsoft.OpenJDK.17 --accept-source-agreements --accept-package-agreements --silent
    $jdk = Get-ChildItem "C:\Program Files\Microsoft" -Directory -Filter "jdk-17*" -ErrorAction SilentlyContinue | Select-Object -First 1
}
if (-not $jdk) { Write-Output "JDK17_INSTALL_FAILED"; exit 1 }
Write-Output "JDK: $($jdk.FullName)"
$env:JAVA_HOME = $jdk.FullName
$env:Path = "$($jdk.FullName)\bin;" + $env:Path

Write-Output "=== STEP 2: Android cmdline-tools ==="
$sdkRoot = "$env:LOCALAPPDATA\Android\Sdk"
New-Item -ItemType Directory -Force -Path $sdkRoot | Out-Null
$sdkmgr = "$sdkRoot\cmdline-tools\latest\bin\sdkmanager.bat"
if (-not (Test-Path $sdkmgr)) {
    $zip = "$env:TEMP\cmdlinetools.zip"
    Invoke-WebRequest -Uri "https://dl.google.com/android/repository/commandlinetools-win-11076708_latest.zip" -OutFile $zip
    $extract = "$env:TEMP\cmdlinetools-extract"
    if (Test-Path $extract) { Remove-Item $extract -Recurse -Force }
    Expand-Archive -Path $zip -DestinationPath $extract -Force
    New-Item -ItemType Directory -Force -Path "$sdkRoot\cmdline-tools" | Out-Null
    Move-Item "$extract\cmdline-tools" "$sdkRoot\cmdline-tools\latest"
}
Write-Output "sdkmanager: $(Test-Path $sdkmgr)"

Write-Output "=== STEP 3: licenses ==="
$ys = ("y`n" * 40)
$ys | & $sdkmgr --licenses | Out-Null

Write-Output "=== STEP 4: packages ==="
& $sdkmgr --install "platform-tools" "platforms;android-35" "build-tools;35.0.0" "ndk;27.2.12479018" "cmake;3.22.1"
Write-Output "=== DONE ==="
Set-Content -Path "$PSScriptRoot\SETUP_DONE.txt" -Value "ok" -Encoding utf8
