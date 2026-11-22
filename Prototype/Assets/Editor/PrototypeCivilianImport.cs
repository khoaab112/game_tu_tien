using System;
using System.Linq;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;

namespace TuTien.Prototype.Editor
{
    internal static class PrototypeCivilianImport
    {
        private const string Folder = "Assets/Art/CharacterSlices";
        private const string CycleFolder = "Assets/Art/CharacterCycles";
        private const string ShadowMaterial = "Assets/Generated/CharacterContactShadow.mat";

        public static PrototypeCivilian AddTo(GameObject player)
        {
            var idleFrames = ImportSheet("Idle", "idle", 3, 257, 671);
            var walkFront = ImportSheet("WalkFront", "walk_front", 4, 165, 421, 868);
            var walkBack = ImportSheet("WalkBack", "walk_back", 4, 155, 413, 854);
            var walkSide = ImportSheet("WalkSide", "walk_side", 4, 155, 415, 860);
            var runFront = ImportSheet("RunFront", "run_front", 4, 166, 425, 869);
            var runBack = ImportSheet("RunBack", "run_back", 4, 157, 417, 861);
            var runSide = ImportSheet("RunSide", "run_side", 4, 157, 417, 861);

            player.GetComponent<Renderer>().enabled = false;
            AddContactShadow(player.transform);
            var art = new GameObject("Civilian sprite");
            art.transform.SetParent(player.transform, false);
            art.transform.localPosition = new Vector3(0, -0.97f, 0);

            var renderer = art.AddComponent<SpriteRenderer>();
            renderer.spriteSortPoint = SpriteSortPoint.Pivot;
            var civilian = art.AddComponent<PrototypeCivilian>();
            var settings = new SerializedObject(civilian);
            var idle = settings.FindProperty("idle");
            SetFrames(idle.FindPropertyRelative("front"), idleFrames[0]);
            SetFrames(idle.FindPropertyRelative("back"), idleFrames[1]);
            SetFrames(idle.FindPropertyRelative("side"), idleFrames[2]);
            var walk = settings.FindProperty("walk");
            SetFrames(walk.FindPropertyRelative("front"), walkFront);
            SetFrames(walk.FindPropertyRelative("back"), walkBack);
            SetFrames(walk.FindPropertyRelative("side"), walkSide);
            var run = settings.FindProperty("run");
            SetFrames(run.FindPropertyRelative("front"), runFront);
            SetFrames(run.FindPropertyRelative("back"), runBack);
            SetFrames(run.FindPropertyRelative("side"), runSide);
            var guard = LoadFrame("punch_side_05");
            var extend = LoadFrame("punch_side_06");
            SetFrames(settings.FindProperty("punchFront"), LoadFrame("punch_front_01"));
            SetFrames(settings.FindProperty("punchSide"), guard, extend, LoadFrame("punch_side_04"),
                LoadFrame("punch_side_01"), extend, guard);
            renderer.sprite = idleFrames[0];
            settings.ApplyModifiedPropertiesWithoutUndo();
            return civilian;
        }

        private static void AddContactShadow(Transform player)
        {
            var material = AssetDatabase.LoadAssetAtPath<Material>(ShadowMaterial);
            if (material == null)
            {
                material = new Material(AssetDatabase.LoadAssetAtPath<Shader>("Assets/Art/CharacterContactShadow.shader"));
                AssetDatabase.CreateAsset(material, ShadowMaterial);
            }

            var shadow = GameObject.CreatePrimitive(PrimitiveType.Quad);
            shadow.name = "Contact shadow";
            shadow.transform.SetParent(player, false);
            shadow.transform.localPosition = new Vector3(0, -0.99f, -0.02f);
            shadow.transform.localRotation = Quaternion.Euler(90, 0, 0);
            shadow.transform.localScale = new Vector3(1.18f, 0.52f, 1);
            shadow.GetComponent<Renderer>().sharedMaterial = material;
            UnityEngine.Object.DestroyImmediate(shadow.GetComponent<Collider>());
        }

        private static Sprite LoadFrame(string name)
        {
            var path = $"{Folder}/{name}.png";
            return AssetDatabase.LoadAssetAtPath<Sprite>(path)
                ?? throw new InvalidOperationException($"Missing character sprite {path}.");
        }

        private static Sprite[] ImportSheet(string name, string prefix, int columns, int scale, params int[] baselines)
        {
            var path = $"{CycleFolder}/{name}.png";
            var importer = (TextureImporter)AssetImporter.GetAtPath(path);
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.spritePixelsPerUnit = scale;
            importer.maxTextureSize = 4096;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.mipmapEnabled = false;
            importer.alphaIsTransparency = true;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            var settings = new TextureImporterSettings();
            importer.ReadTextureSettings(settings);
            settings.spriteMeshType = SpriteMeshType.FullRect;
            importer.SetTextureSettings(settings);
            importer.SaveAndReimport();

            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            var factory = new SpriteDataProviderFactories();
            factory.Init();
            var provider = factory.GetSpriteEditorDataProviderFromObject(importer);
            provider.InitSpriteEditorDataProvider();
            var previous = provider.GetSpriteRects().ToDictionary(sprite => sprite.name, sprite => sprite.spriteID);
            var rects = new SpriteRect[columns * baselines.Length];
            for (var row = 0; row < baselines.Length; row++)
            for (var column = 0; column < columns; column++)
            {
                var index = row * columns + column;
                var spriteName = $"{prefix}_{index + 1:00}";
                var left = column * texture.width / columns;
                var right = (column + 1) * texture.width / columns;
                var top = row * texture.height / baselines.Length;
                var bottom = (row + 1) * texture.height / baselines.Length;
                rects[index] = new SpriteRect
                {
                    name = spriteName,
                    spriteID = previous.TryGetValue(spriteName, out var id) ? id : GUID.Generate(),
                    rect = new Rect(left, texture.height - bottom, right - left, bottom - top),
                    alignment = SpriteAlignment.Custom,
                    pivot = new Vector2(0.5f, (float)(bottom - baselines[row]) / (bottom - top))
                };
            }
            provider.SetSpriteRects(rects);
            provider.GetDataProvider<ISpriteNameFileIdDataProvider>()
                .SetNameFileIdPairs(rects.Select(rect => new SpriteNameFileIdPair(rect.name, rect.spriteID)));
            provider.Apply();
            importer.SaveAndReimport();

            var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToDictionary(sprite => sprite.name);
            return rects.Select(rect => sprites[rect.name]).ToArray();
        }

        private static void SetFrames(SerializedProperty target, params Sprite[] frames)
        {
            target.arraySize = frames.Length;
            for (var index = 0; index < frames.Length; index++)
                target.GetArrayElementAtIndex(index).objectReferenceValue = frames[index];
        }
    }
}
