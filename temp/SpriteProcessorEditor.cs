# Unity Editor Tool: Tự động cắt & chuẩn hóa Spritesheet Top-down Tu Tiên
// Đặt file này vào thư mục: Assets/Editor/SpriteProcessorEditor.cs trong dự án Unity.

using System.IO;
using UnityEditor;
using UnityEngine;

namespace TuTien.EditorTools
{
    public class SpriteProcessorEditor : EditorWindow
    {
        private int frameWidth = 128;
        private int frameHeight = 128;
        private int pixelsPerUnit = 32;
        private Vector2 customPivot = new Vector2(0.5f, 0.12f); // Pivot tại chân nhân vật

        [MenuItem("Tu Tiên/Sprite Processor (128x128 Top-Down)")]
        public static void ShowWindow()
        {
            GetWindow<SpriteProcessorEditor>("Xianxia Sprite Tool");
        }

        private void OnGUI()
        {
            GUILayout.Label("Cấu hình Spritesheet Nhân vật", EditorStyles.boldLabel);
            frameWidth = EditorGUILayout.IntField("Frame Width (px)", frameWidth);
            frameHeight = EditorGUILayout.IntField("Frame Height (px)", frameHeight);
            pixelsPerUnit = EditorGUILayout.IntField("Pixels Per Unit", pixelsPerUnit);
            customPivot = EditorGUILayout.Vector2Field("Pivot (X=Center, Y=Foot)", customPivot);

            EditorGUILayout.Space(10);
            EditorGUILayout.HelpBox(
                "Chọn 1 hoặc nhiều ảnh Spritesheet trong cửa sổ Project rồi nhấn nút bên dưới để tự động:\n" +
                "1. Chuyển sang Sprite (2D), Filter Point (sắc nét không mờ).\n" +
                "2. Cắt lưới 128x128 tự động.\n" +
                "3. Đặt Pivot tại chân nhân vật (Y = 0.12).", 
                MessageType.Info);

            if (GUILayout.Button("Cắt & Cấu hình Spritesheet Đã Chọn", GUILayout.Height(35)))
            {
                ProcessSelectedTextures();
            }
        }

        private void ProcessSelectedTextures()
        {
            var selectedObjects = Selection.objects;
            int count = 0;

            foreach (var obj in selectedObjects)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                if (string.IsNullOrEmpty(path)) continue;

                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                if (importer == null) continue;

                // 1. Cấu hình Texture chuẩn Pixel Art
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Multiple;
                importer.filterMode = FilterMode.Point;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                importer.spritePixelsPerUnit = pixelsPerUnit;

                // 2. Đọc kích thước ảnh để chia lưới 128x128
                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                if (texture == null) continue;

                int columns = texture.width / frameWidth;
                int rows = texture.height / frameHeight;
                int totalFrames = columns * rows;

                SpriteMetaData[] metas = new SpriteMetaData[totalFrames];
                int index = 0;

                for (int r = rows - 1; r >= 0; r--)
                {
                    for (int c = 0; c < columns; c++)
                    {
                        SpriteMetaData meta = new SpriteMetaData();
                        meta.rect = new Rect(c * frameWidth, r * frameHeight, frameWidth, frameHeight);
                        meta.name = $"{Path.GetFileNameWithoutExtension(path)}_{index}";
                        meta.alignment = (int)SpriteAlignment.Custom;
                        meta.pivot = customPivot;
                        metas[index] = meta;
                        index++;
                    }
                }

                importer.spritesheet = metas;
                EditorUtility.SetDirty(importer);
                importer.SaveAndReimport();
                count++;
            }

            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("Hoàn tất", $"Đã cấu hình và cắt thành công {count} spritesheet!", "OK");
        }
    }
}
