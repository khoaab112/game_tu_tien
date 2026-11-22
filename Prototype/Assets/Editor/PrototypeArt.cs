using UnityEditor;
using UnityEngine;

namespace TuTien.Prototype.Editor
{
    internal static class PrototypeArt
    {
        private const string VillageImage = "Assets/Art/StarterVillage.png";
        private const string VillageMaterial = "Assets/Generated/StarterVillage.mat";

        public static void BuildVillage()
        {
            var importer = (TextureImporter)AssetImporter.GetAtPath(VillageImage);
            importer.maxTextureSize = 4096;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.mipmapEnabled = false;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;
            importer.SaveAndReimport();

            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(VillageImage);
            if (texture.width != PrototypeVillage.ImageWidth || texture.height != PrototypeVillage.ImageHeight)
                throw new System.InvalidOperationException("Starter village dimensions must match the layout in PrototypeVillage.");

            var material = AssetDatabase.LoadAssetAtPath<Material>(VillageMaterial);
            if (material == null)
            {
                material = new Material(Shader.Find("Unlit/Texture"));
                AssetDatabase.CreateAsset(material, VillageMaterial);
            }
            material.mainTexture = texture;
            EditorUtility.SetDirty(material);

            var village = GameObject.CreatePrimitive(PrimitiveType.Quad);
            village.name = "Starter village map";
            village.transform.position = new Vector3(0, -0.02f, 0);
            village.transform.rotation = Quaternion.Euler(90, 0, 0);
            village.transform.localScale = new Vector3(PrototypeVillage.Width, PrototypeVillage.Height, 1);
            village.GetComponent<Renderer>().sharedMaterial = material;
            Object.DestroyImmediate(village.GetComponent<Collider>());
            BuildFoliage();
        }

        private static void BuildFoliage()
        {
            var group = new GameObject("Wind foliage").transform;
            // Use the neutral drawing on each sheet. Wind bends the canopy continuously.
            var dense = ImportFoliageSheet("VillageBambooDense");
            var open = ImportFoliageSheet("VillageBambooOpen");
            var tall = ImportFoliageSheet("VillagePineTall");
            var spreading = ImportFoliageSheet("VillagePineSpreading");
            var denseFrame = Rect.MinMaxRect(23, 86, 351, 692);
            var openFrame = Rect.MinMaxRect(36, 85, 393, 606);
            var tallFrame = Rect.MinMaxRect(4, 198, 351, 663);
            var spreadingFrame = Rect.MinMaxRect(22, 144, 455, 584);

            AddTree(group, "West bamboo 1", dense, denseFrame, new Vector2(333, 638), 227, 0.3f, 0.047f);
            AddTree(group, "West bamboo 2", open, openFrame, new Vector2(462, 654), 237, 1.7f, 0.044f);
            AddTree(group, "West bamboo 3", dense, denseFrame, new Vector2(570, 632), 224, 3.1f, 0.045f);
            AddTree(group, "North bamboo 1", open, openFrame, new Vector2(1016, 249), 243, 2.7f, 0.05f);
            AddTree(group, "North bamboo 2", dense, denseFrame, new Vector2(1138, 263), 257, 0.8f, 0.048f);
            AddTree(group, "Shrine pine", tall, tallFrame, new Vector2(1735, 351), 336, 2.1f, 0.032f);
            AddTree(group, "Forge pine", spreading, spreadingFrame, new Vector2(2140, 775), 279, 4.2f, 0.033f);
            AddTree(group, "South bamboo 1", open, openFrame, new Vector2(986, 1324), 252, 1.3f, 0.05f);
            AddTree(group, "South bamboo 2", dense, denseFrame, new Vector2(1070, 1288), 239, 4.9f, 0.046f);
        }

        private static Texture2D ImportFoliageSheet(string name)
        {
            var path = $"Assets/Art/{name}.jpeg";
            var importer = (TextureImporter)AssetImporter.GetAtPath(path);
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.mipmapEnabled = false;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;
            importer.SaveAndReimport();
            return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }

        private static void AddTree(Transform parent, string name, Texture2D sheet, Rect frame,
            Vector2 root, float heightPixels, float phase, float strength)
        {
            var path = $"Assets/Generated/{name}.mat";
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material == null)
            {
                material = new Material(AssetDatabase.LoadAssetAtPath<Shader>("Assets/Art/VillageFoliage.shader"));
                AssetDatabase.CreateAsset(material, path);
            }
            material.mainTexture = sheet;
            material.SetVector("_Frame", new Vector4(frame.xMin / sheet.width, 1 - frame.yMax / sheet.height,
                frame.width / sheet.width, frame.height / sheet.height));
            material.SetVector("_Wind", new Vector4(strength, 1.34f, phase, 0));
            material.SetColor("_Tint", new Color(0.86f, 0.88f, 0.83f));
            EditorUtility.SetDirty(material);

            var foliage = GameObject.CreatePrimitive(PrimitiveType.Quad);
            foliage.name = name;
            foliage.transform.SetParent(parent, true);
            foliage.transform.position = PrototypeVillage.ImageToWorld(root.x, root.y - heightPixels * 0.5f, 0);
            foliage.transform.rotation = Quaternion.Euler(90, 0, 0);
            var height = heightPixels * PrototypeVillage.Width / PrototypeVillage.ImageWidth;
            foliage.transform.localScale = new Vector3(height * frame.width / frame.height * 1.16f, height, 1);
            foliage.GetComponent<Renderer>().sharedMaterial = material;
            Object.DestroyImmediate(foliage.GetComponent<Collider>());
        }
    }
}
