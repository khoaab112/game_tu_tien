using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

public class AnalyzeVillageImages
{
    public static void Main()
    {
        string rawDir = @"D:\coding\GAME-TU-TIEN\Prototype\RawArt";
        string[] files = Directory.GetFiles(rawDir, "*.jpeg");
        Array.Sort(files);

        foreach (var file in files)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("FILE: " + Path.GetFileName(file));
            Analyze(file);
        }
    }

    private static void Analyze(string filePath)
    {
        using (Bitmap bmp = new Bitmap(filePath))
        {
            int w = bmp.Width;
            int h = bmp.Height;

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] bytes = new byte[data.Stride * h];
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
            bmp.UnlockBits(data);

            bool[,] isFg = new bool[w, h];
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int idx = y * data.Stride + x * 4;
                    int b = bytes[idx];
                    int g = bytes[idx + 1];
                    int r = bytes[idx + 2];
                    if (r < 235 || g < 235 || b < 235)
                        isFg[x, y] = true;
                }
            }

            int[] rowCount = new int[h];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    if (isFg[x, y]) rowCount[y]++;

            List<Tuple<int, int>> rowRanges = new List<Tuple<int, int>>();
            bool inRow = false;
            int startY = 0;
            for (int y = 0; y < h; y++)
            {
                if (rowCount[y] > 10)
                {
                    if (!inRow) { inRow = true; startY = y; }
                }
                else
                {
                    if (inRow) { inRow = false; rowRanges.Add(new Tuple<int, int>(startY, y - 1)); }
                }
            }
            if (inRow) rowRanges.Add(new Tuple<int, int>(startY, h - 1));

            Console.WriteLine("Rows: " + rowRanges.Count);
            for (int r = 0; r < rowRanges.Count; r++)
            {
                var row = rowRanges[r];
                Console.WriteLine("  Row " + r + ": Y=" + row.Item1 + ".." + row.Item2 + " (H=" + (row.Item2 - row.Item1 + 1) + ")");

                int[] colCount = new int[w];
                for (int x = 0; x < w; x++)
                    for (int y = row.Item1; y <= row.Item2; y++)
                        if (isFg[x, y]) colCount[x]++;

                List<Tuple<int, int>> colRanges = new List<Tuple<int, int>>();
                bool inCol = false;
                int startX = 0;
                for (int x = 0; x < w; x++)
                {
                    if (colCount[x] > 5)
                    {
                        if (!inCol) { inCol = true; startX = x; }
                    }
                    else
                    {
                        if (inCol) { inCol = false; colRanges.Add(new Tuple<int, int>(startX, x - 1)); }
                    }
                }
                if (inCol) colRanges.Add(new Tuple<int, int>(startX, w - 1));

                Console.WriteLine("    Cols: " + colRanges.Count);
                for (int c = 0; c < colRanges.Count; c++)
                {
                    var col = colRanges[c];
                    int minX = col.Item1, maxX = col.Item2;
                    int minY = row.Item2, maxY = row.Item1;
                    for (int y = row.Item1; y <= row.Item2; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            if (isFg[x, y])
                            {
                                if (y < minY) minY = y;
                                if (y > maxY) maxY = y;
                            }
                        }
                    }
                    Console.WriteLine("      [C" + c + "] X=" + minX + ".." + maxX + " (W=" + (maxX - minX + 1) + "), Y=" + minY + ".." + maxY + " (H=" + (maxY - minY + 1) + ")");
                }
            }
        }
    }
}
