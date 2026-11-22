using System;
using System.IO;
using FishNet.Component.Transforming;
using FishNet.Managing;
using FishNet.Managing.Object;
using FishNet.Object;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

namespace TuTien.Prototype.Editor
{
    public static class PrototypeBuild
    {
        public static void PlayLocalHost()
        {
            EditorSceneManager.OpenScene("Assets/Generated/Prototype.unity");
            EditorApplication.EnterPlaymode();
        }

        public static void Windows()
        {
            Generate();
            Build("Builds/Windows/TuTien.exe", BuildTarget.StandaloneWindows64);
        }

        public static void LinuxServer()
        {
            Generate();
            Build("Builds/LinuxServer/TuTien.x86_64", BuildTarget.StandaloneLinux64, true);
        }

        private static void Build(string output, BuildTarget target, bool server = false)
        {
            var result = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Generated/Prototype.unity" },
                locationPathName = output,
                target = target,
                subtarget = (int)(server ? StandaloneBuildSubtarget.Server : StandaloneBuildSubtarget.Player),
                options = BuildOptions.Development
            });
            if (result.summary.result != BuildResult.Succeeded)
                throw new Exception($"Prototype build failed: {result.summary.result}");
        }

        [MenuItem("Tu Tien/Generate networking prototype")]
        public static void Generate()
        {
            Directory.CreateDirectory("Assets/Generated");
            AssetDatabase.Refresh();
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            PlayerSettings.runInBackground = true;
            PlayerSettings.defaultScreenWidth = 960;
            PlayerSettings.defaultScreenHeight = 640;

            var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "PrototypePlayer";
            player.AddComponent<NetworkObject>();
            var networkTransform = player.AddComponent<NetworkTransform>();
            var transformSettings = new SerializedObject(networkTransform);
            transformSettings.FindProperty("_clientAuthoritative").boolValue = false;
            transformSettings.ApplyModifiedPropertiesWithoutUndo();
            var movement = player.AddComponent<PrototypeMovement>();
            movement.Civilian = PrototypeCivilianImport.AddTo(player);
            var prefab = PrefabUtility.SaveAsPrefabAsset(player, "Assets/Generated/Player.prefab");
            UnityEngine.Object.DestroyImmediate(player);

            var dummy = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            dummy.name = "QA Training Dummy";
            dummy.AddComponent<NetworkObject>();
            dummy.AddComponent<PrototypeDummy>();
            var dummyPrefab = PrefabUtility.SaveAsPrefabAsset(dummy, "Assets/Generated/Dummy.prefab");
            UnityEngine.Object.DestroyImmediate(dummy);

            const string collectionPath = "Assets/Generated/Spawnables.asset";
            var collection = AssetDatabase.LoadAssetAtPath<SinglePrefabObjects>(collectionPath);
            if (collection == null)
            {
                collection = ScriptableObject.CreateInstance<SinglePrefabObjects>();
                AssetDatabase.CreateAsset(collection, collectionPath);
            }
            collection.Clear();
            collection.AddObject(prefab.GetComponent<NetworkObject>());
            collection.AddObject(dummyPrefab.GetComponent<NetworkObject>());
            EditorUtility.SetDirty(collection);

            var managerObject = new GameObject("Network");
            var manager = managerObject.AddComponent<NetworkManager>();
            manager.SpawnablePrefabs = collection;
            var session = managerObject.AddComponent<PrototypeSession>();
            session.Manager = manager;
            session.PlayerPrefab = prefab.GetComponent<NetworkObject>();
            session.DummyPrefab = dummyPrefab.GetComponent<NetworkObject>();

            PrototypeArt.BuildVillage();
            var camera = new GameObject("Camera").AddComponent<Camera>();
            camera.tag = "MainCamera";
            camera.transform.position = new Vector3(0, 36, 0);
            camera.transform.rotation = Quaternion.Euler(90, 0, 0);
            camera.orthographic = true;
            camera.orthographicSize = PrototypeVillage.ViewHalfHeight(1.6f);
            camera.gameObject.AddComponent<PrototypeVillage>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.09f, 0.14f, 0.1f);
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.57f, 0.61f, 0.65f);
            var light = new GameObject("Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.color = new Color(1f, 0.88f, 0.73f);
            light.shadows = LightShadows.Soft;
            light.transform.rotation = Quaternion.Euler(50, -30, 0);
            EditorSceneManager.SaveScene(camera.gameObject.scene, "Assets/Generated/Prototype.unity");
            AssetDatabase.SaveAssets();
        }
    }
}
