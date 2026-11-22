param(
    [Parameter(Mandatory)][string]$Editor,
    [ValidateSet('Windows','LinuxServer')][string]$Target = 'Windows'
)
$ErrorActionPreference = 'Stop'
$project = $PSScriptRoot
$artifacts = Join-Path $project 'Artifacts'
New-Item -ItemType Directory -Force -Path $artifacts | Out-Null
$log = Join-Path $artifacts ("build-" + $Target + ".log")
$buildTarget = 'Win64'
$subtarget = 'Player'
if ($Target -eq 'LinuxServer') {
    $buildTarget = 'Linux64'
    $subtarget = 'Server'
}
$arguments = @('-batchmode', '-nographics', '-quit', '-projectPath', ('"' + $project + '"'),
    '-buildTarget', $buildTarget, '-standaloneBuildSubtarget', $subtarget,
    '-executeMethod', ("TuTien.Prototype.Editor.PrototypeBuild." + $Target), '-logFile', ('"' + $log + '"'))
$process = Start-Process -FilePath $Editor -ArgumentList $arguments -PassThru -Wait -WindowStyle Hidden
if ($process.ExitCode -ne 0) { throw "Unity build failed. Read $log" }
Write-Output "Build complete. Log at $log"
