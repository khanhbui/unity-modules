using System;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Scene.Home;
using Snakat.Common.Attribute;
using Snakat.Scene;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scene.Settings
{
    public class SettingsParam : IParam
    {
    }

    [File("Assets/Game/Scenes/Settings/SettingsScene.unity")]
    public class SettingsScene : Scene<SettingsParam>
    {
        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private Button _homeButton;

        public override async UniTask OnInitialize(SettingsParam param)
        {
            await base.OnInitialize(param);

            _backButton.OnClickAsObservable()
                .Subscribe(_ => OnClickBack())
                .AddTo(this);
            _homeButton.OnClickAsObservable()
                .Subscribe(_ => OnClickHome())
                .AddTo(this);
        }

        private void OnClickBack()
        {
            Debug.Log("BACK");
            SceneManager.Instance.PopScene();
        }

        private void OnClickHome()
        {
            SceneManager.Instance.ResetToScene<HomeScene>();
        }

    }
}
