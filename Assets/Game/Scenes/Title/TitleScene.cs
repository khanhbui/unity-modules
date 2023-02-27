using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Scene.Home;
using Snakat.Common.Attribute;
using Snakat.Scene;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scene.Title
{
    public class TitleParam : IParam
    {
    }

    [File("Assets/Game/Scenes/Title/TitleScene.unity")]
    public class TitleScene : Scene<TitleParam>
    {
        [SerializeField]
        private Button _startButton;

        public override async UniTask OnInitialize(TitleParam param)
        {
            await base.OnInitialize(param);

            _startButton.OnClickAsObservable()
                .Subscribe(_ => OnClickStart())
                .AddTo(this);
        }

        private void OnClickStart()
        {
            SceneManager.Instance.ReplaceScene<HomeScene>();
        }
    }
}
