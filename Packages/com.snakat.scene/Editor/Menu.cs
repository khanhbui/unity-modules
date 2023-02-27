using UnityEditor;

namespace Snakat.Scene.Editor
{
    internal static class Menu
    {
        private const string _ROOT_MENU_ITEM = "Snakat";
        private const string _SCENE_MENU_ITEM = _ROOT_MENU_ITEM + "/Scene";

        [MenuItem(_SCENE_MENU_ITEM + "/Boot Scene Settings")]

        internal static void ShowSettingWindow()
        {
            var window = EditorWindow.GetWindow<BootSceneSettingWindow>(false, "Boot Scene Settings");
            window.Initialize(BootSceneHandler.IsEnabled, BootSceneHandler.BootScenePath);
        }
    }
}
