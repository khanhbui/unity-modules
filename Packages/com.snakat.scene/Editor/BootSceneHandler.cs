using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snakat.Scene.Editor
{
    [InitializeOnLoad]
    internal static class BootSceneHandler
    {
        private static readonly string _prefsKeyEnabled = "snakat.scene.editor.boot_scene.enabled";
        private static readonly string _prefsKeyBootScenePath = "snakat.scene.editor.boot_scene.boot_scene_path";
        private static readonly string _prefsKeyPreviousScenes = "snakat.scene.editor.boot_scene.previous_scenes";

        private static bool? _isEnabled = false;
        private static string _bootScenePath = null;

        internal static bool IsEnabled
        {
            get
            {
                return (_isEnabled.HasValue ? _isEnabled : (_isEnabled = EditorPrefs.GetBool(_prefsKeyEnabled, false))).Value;
            }
            set
            {
                _isEnabled = value;
                EditorPrefs.SetBool(_prefsKeyEnabled, _isEnabled.Value);
            }
        }

        internal static string BootScenePath
        {
            get
            {
                return _bootScenePath ??= EditorPrefs.GetString(_prefsKeyBootScenePath, string.Empty);
            }
            set
            {
                _bootScenePath = value;
                EditorPrefs.SetString(_prefsKeyBootScenePath, _bootScenePath);
            }
        }

        static BootSceneHandler()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                    if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorApplication.isPlaying = false;
                        return;
                    }

                    var previousScenes = new List<string>();
                    for (var i = 0; i < SceneManager.sceneCount; i++)
                    {
                        previousScenes.Add(SceneManager.GetSceneAt(i).path);
                    }
                    EditorPrefs.SetString(_prefsKeyPreviousScenes, string.Join("|", previousScenes));

                    EditorSceneManager.OpenScene(BootScenePath);
                    break;

                case PlayModeStateChange.EnteredEditMode:
                    var scenesToRestore = EditorPrefs.GetString(_prefsKeyPreviousScenes).Split('|');
                    for (var i = 0; i < scenesToRestore.Length; i++)
                    {
                        var scene = scenesToRestore[i];
                        EditorSceneManager.OpenScene(scene, i == 0 ? OpenSceneMode.Single : OpenSceneMode.Additive);
                    }
                    EditorPrefs.DeleteKey(_prefsKeyPreviousScenes);
                    break;

                case PlayModeStateChange.ExitingPlayMode:
                    break;

                case PlayModeStateChange.EnteredPlayMode:
                    break;
            }
        }
    }
}
