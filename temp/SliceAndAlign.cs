using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public class SliceAndAlign
{
    public class SpriteSpec
    {
        public string Action;
        public string Direction;
        public string FileName;
        public int Row;
        public int Col;
        public int MinX, MaxX, MinY, MaxY;
        public int FootX, FootY;
        public int CanvasW = 160;
        public int CanvasH = 160;
        public int TargetAnchorX = 80;
        public int TargetAnchorY = 145;
        public bool Loop = true;
        public int? Fps = 8;
        public string OriginalFacing = "right"; // for side
    }

    public static void Main(string[] args)
    {
        string srcPath = "C:/Users/khoaz/.gemini/antigravity/brain/d2c88038-8782-4807-95d6-e9bf4fe2752f/.user_uploaded/media_1795357808309_2c151066.jpg";
        string outDir = @"D:\coding\GAME-TU-TIEN\temp\character_slices";
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
                        isFg[x, y] = true;
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

            // Extract bounding box of every cell [r, c]
            var cellBoxes = new Dictionary<string, Tuple<int, int, int, int, int, int>>(); // minX, maxX, minY, maxY, footX, footY

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
                    int maxYPixel = 0;

                    for (int y = row.Item1; y <= row.Item2; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            if (isFg[x, y])
                            {
                                if (y < minY) minY = y;
                                if (y > maxY) maxY = y;
                                if (y > maxYPixel) maxYPixel = y;
                            }
                        }
                    }

                    // Calculate the torso / hip center of gravity for stable horizontal alignment
                    int sumX = 0, count = 0;
                    int yTop = minY + (int)((maxY - minY) * 0.25);
                    int yBottom = minY + (int)((maxY - minY) * 0.65);
                    for (int y = yTop; y <= yBottom; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            if (isFg[x, y])
                            {
                                sumX += x;
                                count++;
                            }
                        }
                    }
                    int torsoX = count > 0 ? (int)Math.Round((double)sumX / count) : (minX + maxX) / 2;

                    int footY = maxYPixel;
                    cellBoxes[rowIdx + "_" + c] = Tuple.Create(minX, maxX, minY, maxY, torsoX, footY);
                }
            }

            // Define Specs
            List<SpriteSpec> specs = new List<SpriteSpec>();

            // IDLE
            specs.Add(new SpriteSpec { Action = "idle", Direction = "front", FileName = "idle_front_01.png", Row = 0, Col = 0 });
            specs.Add(new SpriteSpec { Action = "idle", Direction = "front", FileName = "idle_front_02.png", Row = 0, Col = 4 });
            specs.Add(new SpriteSpec { Action = "idle", Direction = "front", FileName = "idle_front_03.png", Row = 0, Col = 6 });

            specs.Add(new SpriteSpec { Action = "idle", Direction = "back", FileName = "idle_back_01.png", Row = 0, Col = 1 });
            specs.Add(new SpriteSpec { Action = "idle", Direction = "back", FileName = "idle_back_02.png", Row = 0, Col = 7 });
            specs.Add(new SpriteSpec { Action = "idle", Direction = "back", FileName = "idle_back_03.png", Row = 0, Col = 8 });

            specs.Add(new SpriteSpec { Action = "idle", Direction = "side", FileName = "idle_side_01.png", Row = 0, Col = 2, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "idle", Direction = "side", FileName = "idle_side_02.png", Row = 0, Col = 3, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "idle", Direction = "side", FileName = "idle_side_03.png", Row = 0, Col = 5, OriginalFacing = "right" });

            // WALK
            specs.Add(new SpriteSpec { Action = "walk", Direction = "front", FileName = "walk_front_01.png", Row = 1, Col = 0 });
            specs.Add(new SpriteSpec { Action = "walk", Direction = "front", FileName = "walk_front_02.png", Row = 1, Col = 6 });

            specs.Add(new SpriteSpec { Action = "walk", Direction = "back", FileName = "walk_back_01.png", Row = 1, Col = 1 });
            specs.Add(new SpriteSpec { Action = "walk", Direction = "back", FileName = "walk_back_02.png", Row = 1, Col = 7 });

            specs.Add(new SpriteSpec { Action = "walk", Direction = "side", FileName = "walk_side_01.png", Row = 1, Col = 2, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "walk", Direction = "side", FileName = "walk_side_02.png", Row = 1, Col = 3, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "walk", Direction = "side", FileName = "walk_side_03.png", Row = 1, Col = 4, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "walk", Direction = "side", FileName = "walk_side_04.png", Row = 1, Col = 5, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "walk", Direction = "side", FileName = "walk_side_05.png", Row = 1, Col = 8, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "walk", Direction = "side", FileName = "walk_side_06.png", Row = 1, Col = 9, OriginalFacing = "right" });

            // RUN
            specs.Add(new SpriteSpec { Action = "run", Direction = "front", FileName = "run_front_01.png", Row = 2, Col = 0, Loop = false, Fps = null }); // single frame static
            specs.Add(new SpriteSpec { Action = "run", Direction = "back", FileName = "run_back_01.png", Row = 2, Col = 1, Loop = false, Fps = null });   // single frame static

            specs.Add(new SpriteSpec { Action = "run", Direction = "side", FileName = "run_side_01.png", Row = 2, Col = 2, OriginalFacing = "right", Fps = 10 });
            specs.Add(new SpriteSpec { Action = "run", Direction = "side", FileName = "run_side_02.png", Row = 2, Col = 3, OriginalFacing = "right", Fps = 10 });
            specs.Add(new SpriteSpec { Action = "run", Direction = "side", FileName = "run_side_03.png", Row = 2, Col = 4, OriginalFacing = "right", Fps = 10 });
            specs.Add(new SpriteSpec { Action = "run", Direction = "side", FileName = "run_side_04.png", Row = 2, Col = 5, OriginalFacing = "right", Fps = 10 });

            // COMBAT STANCE (FIST)
            specs.Add(new SpriteSpec { Action = "combat_stance", Direction = "side", FileName = "combat_stance_side_01.png", Row = 0, Col = 9, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "combat_stance", Direction = "side", FileName = "combat_stance_side_02.png", Row = 0, Col = 10, OriginalFacing = "right" });
            specs.Add(new SpriteSpec { Action = "combat_stance", Direction = "side", FileName = "combat_stance_side_03.png", Row = 1, Col = 10, OriginalFacing = "right" });

            // PUNCH
            specs.Add(new SpriteSpec { Action = "punch", Direction = "side", FileName = "punch_side_01.png", Row = 0, Col = 11, OriginalFacing = "right", Loop = false, Fps = 12 });
            specs.Add(new SpriteSpec { Action = "punch", Direction = "side", FileName = "punch_side_02.png", Row = 1, Col = 11, OriginalFacing = "right", Loop = false, Fps = 12 });
            specs.Add(new SpriteSpec { Action = "punch", Direction = "side", FileName = "punch_side_03.png", Row = 2, Col = 6, OriginalFacing = "right", Loop = false, Fps = 12 });
            specs.Add(new SpriteSpec { Action = "punch", Direction = "front", FileName = "punch_front_01.png", Row = 2, Col = 7, Loop = false, Fps = null });
            specs.Add(new SpriteSpec { Action = "punch", Direction = "side", FileName = "punch_side_04.png", Row = 2, Col = 8, OriginalFacing = "right", Loop = false, Fps = 12 });
            specs.Add(new SpriteSpec { Action = "punch", Direction = "side", FileName = "punch_side_05.png", Row = 2, Col = 9, OriginalFacing = "right", Loop = false, Fps = 12 });
            specs.Add(new SpriteSpec { Action = "punch", Direction = "side", FileName = "punch_side_06.png", Row = 2, Col = 10, OriginalFacing = "right", Loop = false, Fps = 12 });
            specs.Add(new SpriteSpec { Action = "punch", Direction = "side", FileName = "punch_side_07.png", Row = 2, Col = 11, OriginalFacing = "right", Loop = false, Fps = 12 });

            // HURT / FALL
            specs.Add(new SpriteSpec { Action = "hurt_fall", Direction = "side", FileName = "hurt_fall_side_01.png", Row = 3, Col = 0, OriginalFacing = "right", Loop = false, Fps = 8 });
            specs.Add(new SpriteSpec { Action = "hurt_fall", Direction = "side", FileName = "hurt_fall_side_02.png", Row = 3, Col = 1, OriginalFacing = "right", Loop = false, Fps = 8 });
            specs.Add(new SpriteSpec { Action = "hurt_fall", Direction = "side", FileName = "hurt_fall_side_03.png", Row = 3, Col = 2, OriginalFacing = "right", Loop = false, Fps = 8 });
            specs.Add(new SpriteSpec { Action = "hurt_fall", Direction = "side", FileName = "hurt_fall_side_04.png", Row = 3, Col = 3, OriginalFacing = "right", Loop = false, Fps = 8 });
            specs.Add(new SpriteSpec { Action = "hurt_recover", Direction = "side", FileName = "hurt_recover_side_01.png", Row = 3, Col = 4, OriginalFacing = "right", Loop = false, Fps = 8 });

            // MEDITATE
            specs.Add(new SpriteSpec { Action = "meditate", Direction = "front", FileName = "meditate_front_01.png", Row = 3, Col = 5, Loop = true, Fps = null });

            // LIE DOWN / SLEEP / KNOCKED OUT
            specs.Add(new SpriteSpec { Action = "lie_down", Direction = "side", FileName = "lie_down_side_01.png", Row = 3, Col = 6, OriginalFacing = "right", Loop = true, Fps = null });
            specs.Add(new SpriteSpec { Action = "lie_down", Direction = "side", FileName = "lie_down_side_02.png", Row = 3, Col = 7, OriginalFacing = "right", Loop = true, Fps = null });

            // Uniform baseline for standing rows
            int row0Floor = 153;
            int row1Floor = 283;
            int row2Floor = 412;
            int row3Floor = 566;

            int targetCanvasW = 160;
            int targetCanvasH = 160;
            int targetAnchorX = 80;
            int targetAnchorY = 145;

            // Generate images
            foreach (var spec in specs)
            {
                string key = spec.Row + "_" + spec.Col;
                var box = cellBoxes[key];
                int minX = box.Item1, maxX = box.Item2, minY = box.Item3, maxY = box.Item4;
                int footX = box.Item5;
                int footY = box.Item6;

                // Adjust footY to consistent floor line
                int floorY = row0Floor;
                if (spec.Row == 1) floorY = row1Floor;
                else if (spec.Row == 2) floorY = row2Floor;
                else if (spec.Row == 3) floorY = row3Floor;

                // For lie down / meditate, anchor is at bottom center of the figure
                if (spec.Action == "meditate" || spec.Action == "lie_down")
                {
                    floorY = maxY;
                    footX = (minX + maxX) / 2;
                }

                int offsetX = targetAnchorX - footX;
                int offsetY = targetAnchorY - floorY;

                using (Bitmap targetBmp = new Bitmap(targetCanvasW, targetCanvasH, PixelFormat.Format32bppArgb))
                {
                    BitmapData tData = targetBmp.LockBits(new Rectangle(0, 0, targetCanvasW, targetCanvasH), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                    byte[] tBytes = new byte[tData.Stride * targetCanvasH];

                    for (int y = minY; y <= maxY; y++)
                    {
                        int destY = y + offsetY;
                        if (destY < 0 || destY >= targetCanvasH) continue;

                        for (int x = minX; x <= maxX; x++)
                        {
                            int destX = x + offsetX;
                            if (destX < 0 || destX >= targetCanvasW) continue;

                            int srcIdx = y * data.Stride + x * 4;
                            int dstIdx = destY * tData.Stride + destX * 4;

                            byte b = bytes[srcIdx];
                            byte g = bytes[srcIdx + 1];
                            byte r = bytes[srcIdx + 2];

                            int minVal = Math.Min(r, Math.Min(g, b));
                            if (minVal >= 242)
                            {
                                tBytes[dstIdx + 3] = 0; // transparent
                            }
                            else if (minVal >= 200)
                            {
                                double a = (242.0 - minVal) / 42.0;
                                int alpha = (int)Math.Round(a * 255.0);
                                tBytes[dstIdx] = b;
                                tBytes[dstIdx + 1] = g;
                                tBytes[dstIdx + 2] = r;
                                tBytes[dstIdx + 3] = (byte)Math.Max(0, Math.Min(255, alpha));
                            }
                            else
                            {
                                tBytes[dstIdx] = b;
                                tBytes[dstIdx + 1] = g;
                                tBytes[dstIdx + 2] = r;
                                tBytes[dstIdx + 3] = 255;
                            }
                        }
                    }

                    Marshal.Copy(tBytes, 0, tData.Scan0, tBytes.Length);
                    targetBmp.UnlockBits(tData);

                    string savePath = Path.Combine(outDir, spec.FileName);
                    targetBmp.Save(savePath, ImageFormat.Png);
                }
            }

            Console.WriteLine("Saved " + specs.Count + " aligned sprites to " + outDir);

            // Group by Action & Direction to build manifest.json
            var grouped = new Dictionary<string, List<SpriteSpec>>();
            foreach (var spec in specs)
            {
                string gKey = spec.Action + "_" + spec.Direction;
                if (!grouped.ContainsKey(gKey)) grouped[gKey] = new List<SpriteSpec>();
                grouped[gKey].Add(spec);
            }

            StringBuilder json = new StringBuilder();
            json.AppendLine("{");
            json.AppendLine("  \"metadata\": {");
            json.AppendLine("    \"character\": \"civilian_ancient_china\",");
            json.AppendLine("    \"source_resolution\": \"1024x571\",");
            json.AppendLine("    \"slice_mode\": \"non_uniform_bounding_box_aligned\",");
            json.AppendLine("    \"canvas_size\": { \"width\": " + targetCanvasW + ", \"height\": " + targetCanvasH + " },");
            json.AppendLine("    \"default_anchor\": { \"x\": " + targetAnchorX + ", \"y\": " + targetAnchorY + " },");
            json.AppendLine("    \"total_frames\": " + specs.Count);
            json.AppendLine("  },");
            json.AppendLine("  \"animations\": {");

            int gCount = 0;
            foreach (var kvp in grouped)
            {
                gCount++;
                var list = kvp.Value;
                var first = list[0];
                json.AppendLine("    \"" + kvp.Key + "\": {");
                json.AppendLine("      \"action\": \"" + first.Action + "\",");
                json.AppendLine("      \"direction\": \"" + first.Direction + "\",");
                json.AppendLine("      \"is_static_single_frame\": " + (list.Count == 1 ? "true" : "false") + ",");
                json.AppendLine("      \"loop\": " + (first.Loop ? "true" : "false") + ",");
                json.AppendLine("      \"fps\": " + (first.Fps.HasValue ? first.Fps.Value.ToString() : "null") + ",");
                json.AppendLine("      \"original_facing\": " + (first.OriginalFacing != null ? "\"" + first.OriginalFacing + "\"" : "null") + ",");
                json.AppendLine("      \"foot_anchor\": { \"x\": " + targetAnchorX + ", \"y\": " + targetAnchorY + " },");
                json.AppendLine("      \"frames\": [");
                for (int i = 0; i < list.Count; i++)
                {
                    json.Append("        \"" + list[i].FileName + "\"");
                    if (i < list.Count - 1) json.AppendLine(",");
                    else json.AppendLine();
                }
                json.AppendLine("      ]");
                json.Append("    }");
                if (gCount < grouped.Count) json.AppendLine(",");
                else json.AppendLine();
            }

            json.AppendLine("  }");
            json.AppendLine("}");

            string manifestPath = Path.Combine(outDir, "manifest.json");
            File.WriteAllText(manifestPath, json.ToString(), Encoding.UTF8);
            Console.WriteLine("Saved manifest.json successfully!");
        }
    }
}
