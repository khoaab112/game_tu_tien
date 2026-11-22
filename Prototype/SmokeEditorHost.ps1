[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$Editor,
    [ValidateRange(1,65535)][int]$Port = 17773
)

$ErrorActionPreference = 'Stop'
$clientPath = Join-Path $PSScriptRoot 'Builds/Windows/TuTien.exe'
if (-not (Test-Path -LiteralPath $clientPath)) { throw 'Build Windows first with Build.ps1.' }
if (Get-NetUDPEndpoint -LocalPort $Port -ErrorAction SilentlyContinue) {
    throw "UDP $Port is in use. Specify a different -Port."
}

$logs = Join-Path $PSScriptRoot ('Artifacts/editor-host-' + (Get-Date -Format 'yyyyMMdd-HHmmss'))
New-Item -ItemType Directory -Path $logs | Out-Null
$hostLog = Join-Path $logs 'host.log'
$processes = [Collections.Generic.List[Diagnostics.Process]]::new()

function Wait-ForMarker($Process, [string]$Log, [string]$Pattern, [int]$TimeoutSeconds, [int]$AfterLine = 0) {
    $deadline = (Get-Date).AddSeconds($TimeoutSeconds)
    do {
        if ($Process.HasExited) { throw "Test process exited. See $Log" }
        $marker = Select-String -LiteralPath $Log -Pattern $Pattern -ErrorAction SilentlyContinue |
            Where-Object LineNumber -GT $AfterLine | Select-Object -First 1
        if ($marker) { return $marker }
        Start-Sleep -Milliseconds 300
    } until ((Get-Date) -ge $deadline)
    throw "Timed out waiting for $Pattern. See $Log"
}

try {
    $editorProcess = Start-Process -FilePath $Editor -ArgumentList @('-batchmode','-nographics',
        '-projectPath',('"' + $PSScriptRoot + '"'),'-executeMethod',
        'TuTien.Prototype.Editor.PrototypeBuild.PlayLocalHost','--smoke','--port',$Port,
        '-logFile',('"' + $hostLog + '"')) -WindowStyle Hidden -PassThru
    $processes.Add($editorProcess)
    $ownerPattern = '^PROTOTYPE_VISIBLE object=(\d+) owner=True$'
    $hostOwned = (Wait-ForMarker $editorProcess $hostLog $ownerPattern 90).Matches[0].Groups[1].Value
    $null = Wait-ForMarker $editorProcess $hostLog "^PROTOTYPE_MOVED object=$hostOwned$" 15

    foreach ($attempt in 1..2) {
        $startLine = @(Get-Content -LiteralPath $hostLog).Count
        $clientLog = Join-Path $logs "client-$attempt.log"
        $client = Start-Process -FilePath $clientPath -ArgumentList @('--smoke','--port',$Port,
            '-logFile',('"' + $clientLog + '"')) -PassThru
        $processes.Add($client)

        $clientOwned = (Wait-ForMarker $client $clientLog $ownerPattern 20).Matches[0].Groups[1].Value
        if ($clientOwned -eq $hostOwned) { throw "Host and client reported the same owned player. See $logs" }
        $null = Wait-ForMarker $editorProcess $hostLog "^PROTOTYPE_MOVED object=$clientOwned$" 20 $startLine
        $null = Wait-ForMarker $editorProcess $hostLog "^PROTOTYPE_OBSERVED_MOVEMENT object=$clientOwned owner=False$" 20 $startLine
        $null = Wait-ForMarker $client $clientLog "^PROTOTYPE_OBSERVED_MOVEMENT object=$hostOwned owner=False$" 20

        $spawned = @(Select-String -LiteralPath $hostLog -Pattern '^PROTOTYPE_SPAWN client=\d+ object=\d+$' |
            Where-Object LineNumber -GT $startLine)
        $visible = @(Select-String -LiteralPath $clientLog -Pattern '^PROTOTYPE_VISIBLE object=\d+ owner=(True|False)$')
        if ($spawned.Count -ne 1 -or $visible.Count -ne 2) {
            throw "Expected one new server player and exactly two visible players. See $logs"
        }

        $client.Refresh()
        if (-not $client.CloseMainWindow() -or -not $client.WaitForExit(10000)) {
            throw "Client $attempt did not close normally. See $clientLog"
        }
        $null = Wait-ForMarker $editorProcess $hostLog "^PROTOTYPE_QA_DESPAWN object=$clientOwned$" 20 $startLine
    }
    Write-Output "PASS: client joined, moved, left, and rejoined the same Editor host. Logs at $logs"
} finally {
    foreach ($process in $processes) {
        if (-not $process.HasExited) { Stop-Process -Id $process.Id -ErrorAction SilentlyContinue }
    }
}
