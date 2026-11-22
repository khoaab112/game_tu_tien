[CmdletBinding(DefaultParameterSetName='Local')]
param(
    [int]$DurationSeconds = 25,
    [Parameter(ParameterSetName='Local')][switch]$ImpairedNetwork,
    [ValidateRange(1,65535)][int]$Port = 7770,
    [Parameter(Mandatory, ParameterSetName='External')]
    [ValidatePattern('^[a-zA-Z0-9.:-]+$')][string]$Address,
    [Parameter(Mandatory, ParameterSetName='External')][string]$ServerLog
)
$ErrorActionPreference = 'Stop'
$exe = Join-Path $PSScriptRoot 'Builds/Windows/TuTien.exe'
if (-not (Test-Path -LiteralPath $exe)) { throw 'Build Windows first with Build.ps1.' }
if ($DurationSeconds -lt 10) { throw 'DurationSeconds must be at least 10.' }
$folder = Join-Path $PSScriptRoot ('Artifacts/smoke-' + (Get-Date -Format 'yyyyMMdd-HHmmss'))
New-Item -ItemType Directory -Path $folder -Force | Out-Null
$children = [Collections.Generic.List[Diagnostics.Process]]::new()
$networkArgs = @()
if ($ImpairedNetwork) { $networkArgs = @('--qa-network') }
$clientAddress = '127.0.0.1'
$previousServerLog = ''
if ($Address) {
    $clientAddress = $Address
    $previousServerLog = [string](Get-Content -LiteralPath $ServerLog -Raw)
} else {
    $ServerLog = Join-Path $folder 'server.log'
}
try {
    if (-not $Address) {
        $children.Add((Start-Process $exe -ArgumentList (@('-batchmode','-nographics','--server',
            '--port',$Port,'-logFile',('"' + $ServerLog + '"')) + $networkArgs) -PassThru -WindowStyle Hidden))
        $readyBy = (Get-Date).AddSeconds(20)
        do {
            if ($children[0].HasExited) { throw "Server exited. See $ServerLog" }
            $ready = Get-NetUDPEndpoint -LocalPort $Port -ErrorAction SilentlyContinue |
                Where-Object OwningProcess -EQ $children[0].Id
            if (-not $ready) { Start-Sleep -Milliseconds 250 }
        } until ($ready -or (Get-Date) -gt $readyBy)
        if (-not $ready) { throw "Server did not bind UDP $Port. See $ServerLog" }
    }
    foreach ($number in 1..2) {
        $log = Join-Path $folder ("client-$number.log")
        $children.Add((Start-Process $exe -ArgumentList (@('--smoke','--address',$clientAddress,
            '--port',$Port,'-logFile',('"' + $log + '"')) + $networkArgs) -PassThru -WindowStyle Hidden))
    }
    Start-Sleep -Seconds $DurationSeconds
    foreach ($process in $children) {
        if ($process.HasExited) { throw "Process $($process.Id) exited during smoke test." }
    }
} finally {
    foreach ($process in $children) {
        if (-not $process.HasExited) { Stop-Process -Id $process.Id }
    }
}
$server = [string](Get-Content -LiteralPath $ServerLog -Raw)
if (-not $server.StartsWith($previousServerLog, [StringComparison]::Ordinal)) {
    throw "Server log was replaced during the test. See $ServerLog"
}
$server = $server.Substring($previousServerLog.Length)
$networkMarker = 'PROTOTYPE_QA_NETWORK delay_ms=75 unreliable_loss_percent=5'
if ($ImpairedNetwork -and -not $server.Contains($networkMarker)) {
    throw "Server did not activate the QA network profile. See $folder"
}
if ($Address) { Set-Content -LiteralPath (Join-Path $folder 'server.log') -Value $server -Encoding UTF8 }
$spawns = [regex]::Matches($server, '(?m)^PROTOTYPE_SPAWN client=(\d+) object=(\d+)\r?$')
if ($spawns.Count -ne 2 -or $spawns[0].Groups[1].Value -eq $spawns[1].Groups[1].Value -or
    $spawns[0].Groups[2].Value -eq $spawns[1].Groups[2].Value) {
    throw "Expected two distinct players spawned by the server. See $folder"
}
$objectIds = @($spawns[0].Groups[2].Value, $spawns[1].Groups[2].Value)
foreach ($objectId in $objectIds) {
    if ($server -notmatch "(?m)^PROTOTYPE_MOVED object=$objectId\r?$") {
        throw "Server did not report movement for object $objectId. See $folder"
    }
}
$ownedIds = @()
foreach ($number in 1..2) {
    $client = Get-Content -LiteralPath (Join-Path $folder "client-$number.log") -Raw
    if ($ImpairedNetwork -and (-not $client.Contains($networkMarker) -or
        $client -notmatch '(?m)^PROTOTYPE_QA_RTT ms=\d+ tick_hz=\d+\r?$')) {
        throw "Client $number did not activate the QA network profile or record RTT. See $folder"
    }
    if ($client -notmatch '(?m)^PROTOTYPE_VISIBLE object=(\d+) owner=True\r?$') {
        throw "Client $number did not spawn its own player. See $folder"
    }
    $ownedId = $Matches[1]
    if ($ownedId -notin $objectIds -or $ownedId -in $ownedIds) {
        throw "Client $number had an unexpected owned player. See $folder"
    }
    $ownedIds += $ownedId
    foreach ($objectId in $objectIds) {
        $isOwner = $objectId -eq $ownedId
        if ($client -notmatch "(?m)^PROTOTYPE_VISIBLE object=$objectId owner=$isOwner\r?$" -or
            $client -notmatch "(?m)^PROTOTYPE_OBSERVED_MOVEMENT object=$objectId owner=$isOwner\r?$") {
            throw "Client $number did not see object $objectId spawn and move. See $folder"
        }
    }
}
Write-Output "PASS: two clients observed both server-controlled players moving. Logs at $folder"
