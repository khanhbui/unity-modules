using UnityEditor;
using UnityEngine;

namespace Snakat.Scene.Editor
{
    public class BootSceneSettingWindow : EditorWindow
    {
        private bool _isEnabledOriginal = false;
        private Object _bootSceneOriginal;

        private bool _isEnabled = false;
        private Object _bootScene;

        public void Initialize(bool isEnabled, string bootScenePath)
        {
            _isEnabled = isEnabled;
            _bootScene = AssetDatabase.LoadAssetAtPath(bootScenePath, typeof(SceneAsset));

            _isEnabledOriginal = _isEnabled;
            _bootSceneOriginal = _bootScene;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            _isEnabled = EditorGUILayout.Toggle("Enabled", _isEnabled);

            GUI.enabled = _isEnabled;
            _bootScene = EditorGUILayout.ObjectField("Boot Scene", _bootScene, typeof(SceneAsset), false);
            GUI.enabled = true;

            EditorGUILayout.Space();

            var enabled = true;
            if (_isEnabled != _isEnabledOriginal)
            {
                if (_isEnabled && _bootScene == null)
                {
                    enabled = false;
                }
            }
            else if (!_isEnabled)
            {
                enabled = false;
            }
            else if (_bootScene == null)
            {
                enabled = false;
            }
            else if (_bootScene == _bootSceneOriginal)
            {
                enabled = false;
            }
            GUI.enabled = enabled;
            if (GUILayout.Button("Save"))
            {
                BootSceneHandler.IsEnabled = _isEnabled;
                BootSceneHandler.BootScenePath = AssetDatabase.GetAssetOrScenePath(_bootScene);

                _isEnabledOriginal = _isEnabled;
                _bootSceneOriginal = _bootScene;
            }
            GUI.enabled = true;

            EditorGUILayout.EndVertical();
        }

    }
}
