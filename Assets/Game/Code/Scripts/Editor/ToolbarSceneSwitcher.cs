using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace ProjectTD
{
    [InitializeOnLoad]
    public class ToolbarSceneSwitcher
    {
        private static Scene[] _availableScenes;
        private static string[] _scenes;

        public class Scene
        {
            public string Name;
            public string Path;
        }

        static ToolbarSceneSwitcher()
        {
            GetAvailableScenes(out _availableScenes, out _scenes);

            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            int selected = EditorGUILayout.Popup("Scene", -1, _scenes);

            if (selected < 0) return;

            string path = _availableScenes[selected].Path;

            EditorSceneManager.OpenScene(path);
            Debug.Log($"[ToolbarSceneSwitcher]: Selected: {path}");
        }

        private static void GetAvailableScenes(out Scene[] scenes, out string[] sceneList)
        {
            string[] paths = new string[] { "Assets/Game/Scenes" };
            string[] guids = AssetDatabase.FindAssets(string.Empty, paths);

            int initialSize = 10;

            List<Scene> scenePathList = new List<Scene>(initialSize);
            List<string> sceneNameList = new List<string>(initialSize);

            foreach (string guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var obj = AssetDatabase.LoadAssetAtPath(path, typeof(SceneAsset));

                if (obj != null)
                {
                    Scene scene = new Scene();
                    scene.Name = obj.name;
                    scene.Path = path;

                    sceneNameList.Add(obj.name);
                    scenePathList.Add(scene);
                }
            }

            scenes = scenePathList.ToArray();
            sceneList = sceneNameList.ToArray();
        }
    }
}