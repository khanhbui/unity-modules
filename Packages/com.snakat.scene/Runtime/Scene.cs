using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Snakat.Scene
{
    public abstract class Scene<T> : MonoBehaviour, IScene<T> where T: IParam
    {
        public virtual UniTask OnInitialize(T param)
        {
            return UniTask.CompletedTask;
        }

        public UniTask OnInitialize(IParam param)
        {
            return OnInitialize((T)param);
        }

        public virtual UniTask OnBeginTransitionIn(T param)
        {
            return UniTask.CompletedTask;
        }

        public UniTask OnBeginTransitionIn(IParam param)
        {
            return OnBeginTransitionIn((T)param);
        }

        public virtual UniTask OnEndTransitionIn(T param)
        {
            return UniTask.CompletedTask;
        }

        public UniTask OnEndTransitionIn(IParam param)
        {
            return OnEndTransitionIn((T)param);
        }

        public virtual UniTask OnEnter(T param)
        {
            return UniTask.CompletedTask;
        }

        public UniTask OnEnter(IParam param)
        {
            return OnEnter((T)param);
        }

        public virtual UniTask OnPause()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnResume(T param)
        {
            return UniTask.CompletedTask;
        }

        public UniTask OnResume(IParam param)
        {
            return OnResume((T)param);
        }

        public virtual UniTask OnBeginTransitionOut()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnEndTransitionOut()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnExit()
        {
            return UniTask.CompletedTask;
        }

        public void OnDispose()
        {
        }
    }
}
