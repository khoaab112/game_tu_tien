using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public class ProcessMortalVillage
{
    public class AssetInfo
    {
        public string SourceFile;
        public string SubFolder; // Buildings, Props, Foliage, Tiles
        public string FileName;
        public string Category;  // building, prop, foliage, tile
        public int MinX, MaxX, MinY, MaxY;
        public bool IsTile;
        public bool RemoveShadow = true;
        public double PivotX = 0.5;
        public double PivotY = 0.05;
        public bool HasCollider = true;
        public int GridX;
        public int GridY;
    }

    public static void Main()
    {
        string rawDir = @"D:\coding\GAME-TU-TIEN\Prototype\RawArt";
        string baseOut = @"D:\coding\GAME-TU-TIEN\Prototype\Assets\Art\MortalVillage";

        Directory.CreateDirectory(Path.Combine(baseOut, "Buildings"));
        Directory.CreateDirectory(Path.Combine(baseOut, "Props"));
        Directory.CreateDirectory(Path.Combine(baseOut, "Foliage"));
        Directory.CreateDirectory(Path.Combine(baseOut, "Tiles"));

        List<AssetInfo> assets = new List<AssetInfo>();

        // -------------------------------------------------------------
        // 1. Gemini_Generated_Image (7).jpeg: Buildings & Props
        // -------------------------------------------------------------
        string img7 = Path.Combine(rawDir, "Gemini_Generated_Image (7).jpeg");
        // Row 0
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Buildings", FileName = "building_forge_furnace.png", Category = "building", MinX = 89, MaxX = 203, MinY = 9, MaxY = 196, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 14, GridY = 22 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_anvil_stump.png", Category = "prop", MinX = 293, MaxX = 406, MinY = 71, MaxY = 180, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 15, GridY = 21 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_anvil_metal.png", Category = "prop", MinX = 452, MaxX = 555, MinY = 103, MaxY = 173, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 15, GridY = 22 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_weapon_rack.png", Category = "prop", MinX = 616, MaxX = 813, MinY = 45, MaxY = 180, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 16, GridY = 23 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_water_tub_coal.png", Category = "prop", MinX = 878, MaxX = 1050, MinY = 59, MaxY = 186, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 16, GridY = 21 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_blacksmith_bench.png", Category = "prop", MinX = 1101, MaxX = 1301, MinY = 57, MaxY = 172, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 17, GridY = 22 });

        // Row 1
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Buildings", FileName = "building_stall_vegetable.png", Category = "building", MinX = 65, MaxX = 355, MinY = 205, MaxY = 415, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 10, GridY = 15 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Buildings", FileName = "building_stall_tea.png", Category = "building", MinX = 427, MaxX = 647, MinY = 200, MaxY = 404, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 12, GridY = 16 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Buildings", FileName = "building_stall_grain.png", Category = "building", MinX = 719, MaxX = 994, MinY = 201, MaxY = 419, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 14, GridY = 15 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_chest_grain_crates.png", Category = "prop", MinX = 1035, MaxX = 1133, MinY = 225, MaxY = 409, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 11, GridY = 14 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_woven_baskets.png", Category = "prop", MinX = 1138, MaxX = 1223, MinY = 233, MaxY = 409, PivotX = 0.5, PivotY = 0.05, HasCollider = false, GridX = 11, GridY = 16 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_sacks_stacked.png", Category = "prop", MinX = 1229, MaxX = 1330, MinY = 230, MaxY = 409, PivotX = 0.5, PivotY = 0.05, HasCollider = false, GridX = 15, GridY = 14 });

        // Row 2 (Fences & Gates split cleanly into single segments)
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_fence_bamboo_01.png", Category = "prop", MinX = 36, MaxX = 175, MinY = 433, MaxY = 485, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 8, GridY = 12 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_fence_bamboo_01_var.png", Category = "prop", MinX = 36, MaxX = 175, MinY = 503, MaxY = 555, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 8, GridY = 13 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_fence_bamboo_02.png", Category = "prop", MinX = 189, MaxX = 345, MinY = 433, MaxY = 485, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 9, GridY = 12 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_fence_bamboo_02_var.png", Category = "prop", MinX = 189, MaxX = 345, MinY = 503, MaxY = 555, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 9, GridY = 13 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_fence_bamboo_post.png", Category = "prop", MinX = 356, MaxX = 376, MinY = 433, MaxY = 555, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 10, GridY = 12 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_gate_bamboo_closed.png", Category = "prop", MinX = 387, MaxX = 474, MinY = 433, MaxY = 555, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 10, GridY = 13 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_gate_bamboo_open.png", Category = "prop", MinX = 488, MaxX = 604, MinY = 433, MaxY = 555, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 10, GridY = 14 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_gate_bamboo_single.png", Category = "prop", MinX = 623, MaxX = 717, MinY = 433, MaxY = 555, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 10, GridY = 15 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_fence_bamboo_short.png", Category = "prop", MinX = 731, MaxX = 819, MinY = 429, MaxY = 555, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 11, GridY = 12 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_fence_wood_broken.png", Category = "prop", MinX = 853, MaxX = 1022, MinY = 446, MaxY = 540, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 18, GridY = 10 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_fence_wood_picket.png", Category = "prop", MinX = 1048, MaxX = 1186, MinY = 443, MaxY = 545, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 19, GridY = 10 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_wood_poles.png", Category = "prop", MinX = 1217, MaxX = 1339, MinY = 440, MaxY = 545, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 20, GridY = 10 });

        // Row 3
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Buildings", FileName = "building_bridge_wood.png", Category = "building", MinX = 41, MaxX = 262, MinY = 572, MaxY = 751, PivotX = 0.5, PivotY = 0.1, HasCollider = false, GridX = 24, GridY = 12 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_firewood_pile.png", Category = "prop", MinX = 299, MaxX = 405, MinY = 578, MaxY = 746, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 13, GridY = 22 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_firewood_bundles.png", Category = "prop", MinX = 420, MaxX = 525, MinY = 592, MaxY = 751, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 13, GridY = 23 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_wine_jars_set.png", Category = "prop", MinX = 561, MaxX = 748, MinY = 598, MaxY = 742, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 12, GridY = 17 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_crates_barrels_stack.png", Category = "prop", MinX = 788, MaxX = 996, MinY = 589, MaxY = 746, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 15, GridY = 16 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Buildings", FileName = "building_water_well.png", Category = "building", MinX = 1037, MaxX = 1178, MinY = 595, MaxY = 745, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 12, GridY = 10 });
        assets.Add(new AssetInfo { SourceFile = img7, SubFolder = "Props", FileName = "prop_rocks_set.png", Category = "prop", MinX = 1196, MaxX = 1339, MinY = 593, MaxY = 741, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 22, GridY = 8 });

        // -------------------------------------------------------------
        // 2. Gemini_Generated_Image (5).jpeg: Foliage & Ground Tiles
        // -------------------------------------------------------------
        string img5 = Path.Combine(rawDir, "Gemini_Generated_Image (5).jpeg");
        // Row 0
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_pine_tall.png", Category = "foliage", MinX = 26, MaxX = 237, MinY = 14, MaxY = 257, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 5, GridY = 25 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_pine_curved.png", Category = "foliage", MinX = 260, MaxX = 479, MinY = 27, MaxY = 256, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 8, GridY = 25 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_bamboo_cluster.png", Category = "foliage", MinX = 507, MaxX = 679, MinY = 22, MaxY = 252, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 25, GridY = 20 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_tree_large.png", Category = "foliage", MinX = 707, MaxX = 925, MinY = 20, MaxY = 251, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 6, GridY = 8 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_tree_medium.png", Category = "foliage", MinX = 953, MaxX = 1142, MinY = 43, MaxY = 251, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 18, GridY = 6 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_bush_dense.png", Category = "foliage", MinX = 1164, MaxX = 1244, MinY = 170, MaxY = 250, PivotX = 0.5, PivotY = 0.05, HasCollider = false, GridX = 7, GridY = 10 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_bush_round.png", Category = "foliage", MinX = 1265, MaxX = 1339, MinY = 184, MaxY = 248, PivotX = 0.5, PivotY = 0.05, HasCollider = false, GridX = 9, GridY = 10 });

        // Row 1 (Garden beds - protect interior flower pixels)
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_garden_bed_01.png", Category = "foliage", MinX = 392, MaxX = 657, MinY = 277, MaxY = 452, PivotX = 0.5, PivotY = 0.05, HasCollider = true, RemoveShadow = false, GridX = 8, GridY = 18 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Foliage", FileName = "foliage_garden_bed_02.png", Category = "foliage", MinX = 721, MaxX = 980, MinY = 277, MaxY = 452, PivotX = 0.5, PivotY = 0.05, HasCollider = true, RemoveShadow = false, GridX = 11, GridY = 18 });

        // Row 2 (Tiles)
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Tiles", FileName = "tile_grass_meadow.png", Category = "tile", MinX = 56, MaxX = 344, MinY = 472, MaxY = 755, PivotX = 0.5, PivotY = 0.5, HasCollider = false, IsTile = true, GridX = 0, GridY = 0 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Tiles", FileName = "tile_dirt_crossroad_01.png", Category = "tile", MinX = 382, MaxX = 669, MinY = 472, MaxY = 755, PivotX = 0.5, PivotY = 0.5, HasCollider = false, IsTile = true, GridX = 12, GridY = 12 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Tiles", FileName = "tile_dirt_crossroad_02.png", Category = "tile", MinX = 706, MaxX = 993, MinY = 472, MaxY = 755, PivotX = 0.5, PivotY = 0.5, HasCollider = false, IsTile = true, GridX = 13, GridY = 12 });
        assets.Add(new AssetInfo { SourceFile = img5, SubFolder = "Tiles", FileName = "tile_pond_shore.png", Category = "tile", MinX = 1031, MaxX = 1319, MinY = 472, MaxY = 755, PivotX = 0.5, PivotY = 0.5, HasCollider = true, IsTile = true, GridX = 24, GridY = 12 });

        // -------------------------------------------------------------
        // 3. Gemini_Generated_Image (8).jpeg: Tree Sway Animations
        // -------------------------------------------------------------
        string img8 = Path.Combine(rawDir, "Gemini_Generated_Image (8).jpeg");
        int[][] r0Cols = new int[][] { new int[] { 27, 237 }, new int[] { 308, 529 }, new int[] { 577, 792 }, new int[] { 842, 1063 }, new int[] { 1118, 1329 } };
        for (int i = 0; i < 5; i++)
        {
            assets.Add(new AssetInfo { SourceFile = img8, SubFolder = "Foliage", FileName = string.Format("foliage_pine_sway_{0:D2}.png", i + 1), Category = "foliage", MinX = r0Cols[i][0], MaxX = r0Cols[i][1], MinY = 11, MaxY = 263, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 5, GridY = 25 });
        }
        int[][] r1Cols = new int[][] { new int[] { 80, 253 }, new int[] { 343, 517 }, new int[] { 608, 784 }, new int[] { 876, 1050 }, new int[] { 1141, 1315 } };
        for (int i = 0; i < 5; i++)
        {
            assets.Add(new AssetInfo { SourceFile = img8, SubFolder = "Foliage", FileName = string.Format("foliage_bamboo_sway_{0:D2}.png", i + 1), Category = "foliage", MinX = r1Cols[i][0], MaxX = r1Cols[i][1], MinY = 272, MaxY = 503, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 25, GridY = 20 });
        }
        int[][] r2Cols = new int[][] { new int[] { 58, 281 }, new int[] { 332, 531 }, new int[] { 595, 793 }, new int[] { 857, 1056 }, new int[] { 1124, 1320 } };
        for (int i = 0; i < 5; i++)
        {
            assets.Add(new AssetInfo { SourceFile = img8, SubFolder = "Foliage", FileName = string.Format("foliage_tree_sway_{0:D2}.png", i + 1), Category = "foliage", MinX = r2Cols[i][0], MaxX = r2Cols[i][1], MinY = 513, MaxY = 752, PivotX = 0.5, PivotY = 0.05, HasCollider = true, GridX = 6, GridY = 8 });
        }

        // -------------------------------------------------------------
        // 4. Gemini_Generated_Image (9).jpeg: Bush & Water Animations
        // -------------------------------------------------------------
        string img9 = Path.Combine(rawDir, "Gemini_Generated_Image (9).jpeg");
        int[][] r0Bushes = new int[][] { new int[] { 154, 287 }, new int[] { 393, 522 }, new int[] { 623, 755 }, new int[] { 854, 986 }, new int[] { 1089, 1222 } };
        for (int i = 0; i < 5; i++)
        {
            assets.Add(new AssetInfo { SourceFile = img9, SubFolder = "Foliage", FileName = string.Format("foliage_bush_sway_{0:D2}.png", i + 1), Category = "foliage", MinX = r0Bushes[i][0], MaxX = r0Bushes[i][1], MinY = 85, MaxY = 205, PivotX = 0.5, PivotY = 0.05, HasCollider = false, GridX = 7, GridY = 10 });
        }
        int[][] r1Gardens = new int[][] { new int[] { 67, 333 }, new int[] { 391, 657 }, new int[] { 721, 980 }, new int[] { 1046, 1305 } };
        for (int i = 0; i < 4; i++)
        {
            assets.Add(new AssetInfo { SourceFile = img9, SubFolder = "Foliage", FileName = string.Format("foliage_garden_sway_{0:D2}.png", i + 1), Category = "foliage", MinX = r1Gardens[i][0], MaxX = r1Gardens[i][1], MinY = 278, MaxY = 453, PivotX = 0.5, PivotY = 0.05, HasCollider = true, RemoveShadow = false, GridX = 8, GridY = 18 });
        }
        int[][] r2Water = new int[][] { new int[] { 57, 344 }, new int[] { 382, 669 }, new int[] { 706, 994 }, new int[] { 1031, 1319 } };
        for (int i = 0; i < 4; i++)
        {
            assets.Add(new AssetInfo { SourceFile = img9, SubFolder = "Tiles", FileName = string.Format("tile_water_anim_{0:D2}.png", i + 1), Category = "tile", MinX = r2Water[i][0], MaxX = r2Water[i][1], MinY = 472, MaxY = 756, PivotX = 0.5, PivotY = 0.5, HasCollider = true, IsTile = true, GridX = 24, GridY = 12 });
        }

        Console.WriteLine("Total Assets to process: " + assets.Count);

        var bySource = new Dictionary<string, List<AssetInfo>>();
        foreach (var a in assets)
        {
            if (!bySource.ContainsKey(a.SourceFile)) bySource[a.SourceFile] = new List<AssetInfo>();
            bySource[a.SourceFile].Add(a);
        }

        int processedCount = 0;

        foreach (var kvp in bySource)
        {
            string srcPath = kvp.Key;
            var list = kvp.Value;
            Console.WriteLine("Processing " + list.Count + " assets from " + Path.GetFileName(srcPath));

            using (Bitmap srcBmp = new Bitmap(srcPath))
            {
                int w = srcBmp.Width;
                int h = srcBmp.Height;

                BitmapData sData = srcBmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                byte[] srcBytes = new byte[sData.Stride * h];
                Marshal.Copy(sData.Scan0, srcBytes, 0, srcBytes.Length);
                srcBmp.UnlockBits(sData);

                foreach (var item in list)
                {
                    int cropW = item.MaxX - item.MinX + 1;
                    int cropH = item.MaxY - item.MinY + 1;

                    using (Bitmap dstBmp = new Bitmap(cropW, cropH, PixelFormat.Format32bppArgb))
                    {
                        BitmapData dData = dstBmp.LockBits(new Rectangle(0, 0, cropW, cropH), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                        byte[] dstBytes = new byte[dData.Stride * cropH];

                        // Copy raw cropped pixels
                        for (int cy = 0; cy < cropH; cy++)
                        {
                            int sy = item.MinY + cy;
                            for (int cx = 0; cx < cropW; cx++)
                            {
                                int sx = item.MinX + cx;
                                int sIdx = sy * sData.Stride + sx * 4;
                                int dIdx = cy * dData.Stride + cx * 4;

                                dstBytes[dIdx] = srcBytes[sIdx];         // B
                                dstBytes[dIdx + 1] = srcBytes[sIdx + 1]; // G
                                dstBytes[dIdx + 2] = srcBytes[sIdx + 2]; // R
                                dstBytes[dIdx + 3] = 255;                // A
                            }
                        }

                        // Process non-tile assets
                        if (!item.IsTile)
                        {
                            for (int cy = 0; cy < cropH; cy++)
                            {
                                for (int cx = 0; cx < cropW; cx++)
                                {
                                    int dIdx = cy * dData.Stride + cx * 4;
                                    byte b = dstBytes[dIdx];
                                    byte g = dstBytes[dIdx + 1];
                                    byte r = dstBytes[dIdx + 2];

                                    int minVal = Math.Min(r, Math.Min(g, b));
                                    int maxVal = Math.Max(r, Math.Max(g, b));
                                    int diff = maxVal - minVal;

                                    // 1. Pure & near-white background removal (works everywhere, including inside enclosed holes)
                                    if (minVal >= 220 && diff <= 22)
                                    {
                                        dstBytes[dIdx + 3] = 0; // completely transparent
                                    }
                                    else if (minVal >= 190 && diff <= 22)
                                    {
                                        // Soft anti-aliasing edge
                                        double a = (220.0 - minVal) / 30.0;
                                        int alpha = (int)Math.Round(a * 255.0);
                                        dstBytes[dIdx + 3] = (byte)Math.Max(0, Math.Min(255, alpha));
                                    }
                                    // 2. Flat gray shadow removal (neutral gray smudges at bottom/sides)
                                    else if (item.RemoveShadow && minVal >= 125 && maxVal <= 180 && diff <= 10)
                                    {
                                        dstBytes[dIdx + 3] = 0;
                                    }
                                }
                            }
                        }

                        Marshal.Copy(dstBytes, 0, dData.Scan0, dstBytes.Length);
                        dstBmp.UnlockBits(dData);

                        string targetPath = Path.Combine(baseOut, item.SubFolder, item.FileName);
                        dstBmp.Save(targetPath, ImageFormat.Png);
                        processedCount++;
                    }
                }
            }
        }

        Console.WriteLine("Saved " + processedCount + " assets to " + baseOut);

        // Generate asset_manifest.json
        StringBuilder json = new StringBuilder();
        json.AppendLine("{");
        json.AppendLine("  \"metadata\": {");
        json.AppendLine("    \"project\": \"GAME-TU-TIEN\",");
        json.AppendLine("    \"location\": \"MortalVillage (Tan Thu Thon)\",");
        json.AppendLine("    \"total_assets\": " + assets.Count + ",");
        json.AppendLine("    \"categories\": [ \"building\", \"prop\", \"foliage\", \"tile\" ]");
        json.AppendLine("  },");
        json.AppendLine("  \"assets\": [");

        for (int i = 0; i < assets.Count; i++)
        {
            var a = assets[i];
            string relPath = "Assets/Art/MortalVillage/" + a.SubFolder + "/" + a.FileName;
            json.AppendLine("    {");
            json.AppendLine("      \"file_path\": \"" + relPath.Replace("\\", "/") + "\",");
            json.AppendLine("      \"category\": \"" + a.Category + "\",");
            json.AppendLine("      \"pivot\": [" + a.PivotX.ToString("0.0#") + ", " + a.PivotY.ToString("0.0#") + "],");
            json.AppendLine("      \"has_collider\": " + (a.HasCollider ? "true" : "false") + ",");
            json.AppendLine("      \"suggested_grid_pos\": [" + a.GridX + ", " + a.GridY + "]");
            json.Append("    }");
            if (i < assets.Count - 1) json.AppendLine(",");
            else json.AppendLine();
        }

        json.AppendLine("  ]");
        json.AppendLine("}");

        string manifestPath = Path.Combine(baseOut, "asset_manifest.json");
        File.WriteAllText(manifestPath, json.ToString(), Encoding.UTF8);
        Console.WriteLine("Saved asset_manifest.json successfully at " + manifestPath);
    }
}
