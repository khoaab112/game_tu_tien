using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public class SpriteAnalysis
{
    public static void Main(string[] args)
    {
        string path = args.Length > 0 ? args[0] : "C:/Users/khoaz/.gemini/antigravity/brain/d2c88038-8782-4807-95d6-e9bf4fe2752f/.user_uploaded/media_1795357808309_2c151066.jpg";
        using (Bitmap bmp = new Bitmap(path))
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
                    {
                        isFg[x, y] = true;
                    }
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

            Console.WriteLine("Total Rows: " + rowRanges.Count);
            for (int r = 0; r < rowRanges.Count; r++)
            {
                var row = rowRanges[r];
                Console.WriteLine("--- ROW " + r + ": Y=" + row.Item1 + ".." + row.Item2 + " (H=" + (row.Item2 - row.Item1 + 1) + ") ---");
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

                Console.WriteLine("Columns: " + colRanges.Count);
                for (int c = 0; c < colRanges.Count; c++)
                {
                    var col = colRanges[c];
                    int minX = col.Item1, maxX = col.Item2;
                    int minY = row.Item2, maxY = row.Item1;
                    int footY = 0;
                    for (int y = row.Item1; y <= row.Item2; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            if (isFg[x, y])
                            {
                                if (y < minY) minY = y;
                                if (y > maxY) maxY = y;
                                if (y > footY) footY = y;
                            }
                        }
                    }
                    Console.WriteLine("  [R" + r + "C" + c + "] X=" + minX + ".." + maxX + " (W=" + (maxX - minX + 1) + "), Y=" + minY + ".." + maxY + " (H=" + (maxY - minY + 1) + "), FootY=" + footY);
                }
            }
        }
    }
}
