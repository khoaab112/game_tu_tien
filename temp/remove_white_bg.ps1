Add-Type -AssemblyName System.Drawing
$srcPath = "temp/civilian_sample.png"
$dstPath = "temp/civilian_transparent.png"

$bmp = [System.Drawing.Bitmap]::new($srcPath)
$w = $bmp.Width
$h = $bmp.Height

$rect = [System.Drawing.Rectangle]::new(0, 0, $w, $h)
$data = $bmp.LockBits($rect, [System.Drawing.Imaging.ImageLockMode]::ReadWrite, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
$ptr = $data.Scan0
$bytes = [byte[]]::new($data.Stride * $h)
[System.Runtime.InteropServices.Marshal]::Copy($ptr, $bytes, 0, $bytes.Length)

$visited = [bool[]]::new($w * $h)
$queue = [System.Collections.Generic.Queue[int]]::new()

# Seed borders
for ($x = 0; $x -lt $w; $x++) {
    $queue.Enqueue($x)
    $queue.Enqueue(($h - 1) * $w + $x)
    $visited[$x] = $true
    $visited[($h - 1) * $w + $x] = $true
}
for ($y = 1; $y -lt $h - 1; $y++) {
    $idx1 = $y * $w
    $idx2 = $y * $w + ($w - 1)
    $queue.Enqueue($idx1)
    $queue.Enqueue($idx2)
    $visited[$idx1] = $true
    $visited[$idx2] = $true
}

while ($queue.Count -gt 0) {
    $curr = $queue.Dequeue()
    $byteIdx = $curr * 4
    $b = [int]$bytes[$byteIdx]
    $g = [int]$bytes[$byteIdx + 1]
    $r = [int]$bytes[$byteIdx + 2]
    
    $minVal = [Math]::Min($r, [Math]::Min($g, $b))
    if ($minVal -ge 235) {
        $bytes[$byteIdx + 3] = 0
        $cx = $curr % $w
        $cy = [int]($curr / $w)
        
        if ($cx -gt 0 -and -not $visited[$curr - 1]) { $visited[$curr - 1] = $true; $queue.Enqueue($curr - 1) }
        if ($cx -lt $w - 1 -and -not $visited[$curr + 1]) { $visited[$curr + 1] = $true; $queue.Enqueue($curr + 1) }
        if ($cy -gt 0 -and -not $visited[$curr - $w]) { $visited[$curr - $w] = $true; $queue.Enqueue($curr - $w) }
        if ($cy -lt $h - 1 -and -not $visited[$curr + $w]) { $visited[$curr + $w] = $true; $queue.Enqueue($curr + $w) }
    }
    elseif ($minVal -ge 200) {
        $alphaRatio = (255.0 - $minVal) / 55.0
        $calcA = [int][Math]::Round($alphaRatio * 255.0)
        if ($calcA -lt 0) { $calcA = 0 }
        if ($calcA -gt 255) { $calcA = 255 }
        $bytes[$byteIdx + 3] = [byte]$calcA
    }
}

[System.Runtime.InteropServices.Marshal]::Copy($bytes, 0, $ptr, $bytes.Length)
$bmp.UnlockBits($data)
$bmp.Save($dstPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()
Write-Host "Processed civilian_transparent.png successfully"
