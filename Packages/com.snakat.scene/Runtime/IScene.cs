using Cysharp.Threading.Tasks;

namespace Snakat.Scene
{
    public interface IScene
    {
        UniTask OnInitialize(IParam param);

        UniTask OnBeginTransitionIn(IParam param);
        UniTask OnEndTransitionIn(IParam param);

        UniTask OnEnter(IParam param);

        UniTask OnPause();

        UniTask OnResume(IParam param);
        
        UniTask OnBeginTransitionOut();
        UniTask OnEndTransitionOut();

        UniTask OnExit();

        void OnDispose();
    }

    public interface IScene<T> : IScene where T: IParam
    {
        UniTask OnInitialize(T param);

        UniTask OnBeginTransitionIn(T param);
        UniTask OnEndTransitionIn(T param);

        UniTask OnEnter(T param);

        UniTask OnResume(T param);
    }
}
