using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Scene.Settings;
using Snakat.Common.Attribute;
using Snakat.Scene;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scene.Home
{
    public class HomeParam : IParam
    {
    }

    [File("Assets/Game/Scenes/Home/HomeScene.unity")]
    public class HomeScene : Scene<HomeParam>
    {
        [SerializeField]
        private Button _settingsButton;

        public override async UniTask OnInitialize(HomeParam param)
        {
            await base.OnInitialize(param);

            _settingsButton.OnClickAsObservable()
                .Subscribe(_ => OnClickSettings())
                .AddTo(this);
        }

        private void OnClickSettings()
        {
            SceneManager.Instance.PushScene<SettingsScene>();
        }
    }
}
