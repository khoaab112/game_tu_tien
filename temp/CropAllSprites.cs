using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

public class CropAllSprites
{
    public static void Main(string[] args)
    {
        string srcPath = "C:/Users/khoaz/.gemini/antigravity/brain/d2c88038-8782-4807-95d6-e9bf4fe2752f/.user_uploaded/media_1795357808309_2c151066.jpg";
        string outDir = "d:/coding/GAME-TU-TIEN/temp/raw_crops";
        Directory.CreateDirectory(outDir);

        using (Bitmap bmp = new Bitmap(srcPath))
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
                    if (r < 238 || g < 238 || b < 238)
                    {
                        isFg[x, y] = true;
                    }
                }
            }

            // Find rows
            int[] rowCount = new int[h];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    if (isFg[x, y]) rowCount[y]++;

            List<Tuple<int, int>> rowRanges = new List<Tuple<int, int>>();
            bool inRow = false;
            int startY = 0;
            for (int y = 0; y < h; y++)
            {
                if (rowCount[y] > 5)
                {
                    if (!inRow) { inRow = true; startY = y; }
                }
                else
                {
                    if (inRow) { inRow = false; rowRanges.Add(new Tuple<int, int>(startY, y - 1)); }
                }
            }
            if (inRow) rowRanges.Add(new Tuple<int, int>(startY, h - 1));

            for (int rowIdx = 0; rowIdx < rowRanges.Count; rowIdx++)
            {
                var row = rowRanges[rowIdx];
                int[] colCount = new int[w];
                for (int x = 0; x < w; x++)
                    for (int y = row.Item1; y <= row.Item2; y++)
                        if (isFg[x, y]) colCount[x]++;

                List<Tuple<int, int>> colRanges = new List<Tuple<int, int>>();
                bool inCol = false;
                int startX = 0;
                for (int x = 0; x < w; x++)
                {
                    if (colCount[x] > 2)
                    {
                        if (!inCol) { inCol = true; startX = x; }
                    }
                    else
                    {
                        if (inCol) { inCol = false; colRanges.Add(new Tuple<int, int>(startX, x - 1)); }
                    }
                }
                if (inCol) colRanges.Add(new Tuple<int, int>(startX, w - 1));

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

                    int cropW = maxX - minX + 1;
                    int cropH = maxY - minY + 1;

                    using (Bitmap crop = new Bitmap(cropW, cropH, PixelFormat.Format32bppArgb))
                    {
                        BitmapData cData = crop.LockBits(new Rectangle(0, 0, cropW, cropH), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                        byte[] cBytes = new byte[cData.Stride * cropH];

                        for (int cy = 0; cy < cropH; cy++)
                        {
                            for (int cx = 0; cx < cropW; cx++)
                            {
                                int srcIdx = (minY + cy) * data.Stride + (minX + cx) * 4;
                                int dstIdx = cy * cData.Stride + cx * 4;

                                byte b = bytes[srcIdx];
                                byte g = bytes[srcIdx + 1];
                                byte r = bytes[srcIdx + 2];

                                int minVal = Math.Min(r, Math.Min(g, b));
                                if (minVal >= 242)
                                {
                                    cBytes[dstIdx + 3] = 0;
                                }
                                else if (minVal >= 200)
                                {
                                    double a = (242.0 - minVal) / 42.0;
                                    int alpha = (int)Math.Round(a * 255.0);
                                    cBytes[dstIdx] = b;
                                    cBytes[dstIdx + 1] = g;
                                    cBytes[dstIdx + 2] = r;
                                    cBytes[dstIdx + 3] = (byte)Math.Max(0, Math.Min(255, alpha));
                                }
                                else
                                {
                                    cBytes[dstIdx] = b;
                                    cBytes[dstIdx + 1] = g;
                                    cBytes[dstIdx + 2] = r;
                                    cBytes[dstIdx + 3] = 255;
                                }
                            }
                        }

                        Marshal.Copy(cBytes, 0, cData.Scan0, cBytes.Length);
                        crop.UnlockBits(cData);
                        crop.Save(Path.Combine(outDir, string.Format("R{0}_C{1:D2}.png", rowIdx, c)), ImageFormat.Png);
                    }
                }
            }
        }
        Console.WriteLine("All cropped to raw_crops successfully!");
    }
}
