[CmdletBinding()]
param([ValidateRange(1,65535)][int]$Port = 7770)

$ErrorActionPreference = 'Stop'
$exe = Join-Path $PSScriptRoot 'Builds/Windows/TuTien.exe'
if (-not (Test-Path -LiteralPath $exe)) {
    throw 'Build the Windows player with Build.ps1 before running the demo.'
}
if (Get-NetUDPEndpoint -LocalPort $Port -ErrorAction SilentlyContinue) {
    throw "UDP port $Port is already in use. Run again with -Port 17770."
}

$logDir = Join-Path $PSScriptRoot ('Artifacts/demo-' + (Get-Date -Format 'yyyyMMdd-HHmmss'))
New-Item -ItemType Directory -Path $logDir -Force | Out-Null
$processes = [Collections.Generic.List[Diagnostics.Process]]::new()
try {
    $log = Join-Path $logDir 'server.log'
    $server = Start-Process $exe -ArgumentList @('--server','-batchmode','-nographics',
        '--port',$Port,'-logFile',('"' + $log + '"')) -WindowStyle Hidden -PassThru
    $processes.Add($server)
    $deadline = (Get-Date).AddSeconds(20)
    do {
        if ($server.HasExited) { throw "Server exited. See $log" }
        $ready = Get-NetUDPEndpoint -LocalPort $Port -ErrorAction SilentlyContinue |
            Where-Object OwningProcess -EQ $server.Id
        if (-not $ready) { Start-Sleep -Milliseconds 250 }
    } until ($ready -or (Get-Date) -ge $deadline)
    if (-not $ready) { throw "Server did not bind UDP $Port. See $log" }

    foreach ($number in 1..2) {
        $log = Join-Path $logDir "client-$number.log"
        $client = Start-Process $exe -ArgumentList @('--port',$Port,
            '-screen-fullscreen','0','-screen-width','800','-screen-height','500',
            '-logFile',('"' + $log + '"')) -PassThru
        $processes.Add($client)
    }
    Write-Host 'Use WASD or the arrow keys in either client. Close both windows to stop the local server.'
    Write-Host "Logs: $logDir"
    while (-not $processes[1].HasExited -or -not $processes[2].HasExited) {
        if ($server.HasExited) { throw "Server exited during the demo. See $logDir" }
        Start-Sleep -Milliseconds 500
    }
} finally {
    foreach ($process in $processes) {
        if (-not $process.HasExited) { Stop-Process -Id $process.Id -ErrorAction SilentlyContinue }
    }
}
