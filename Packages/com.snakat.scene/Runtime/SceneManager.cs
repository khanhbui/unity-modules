using Cysharp.Threading.Tasks;
using Snakat.Common;

namespace Snakat.Scene
{
    public partial class SceneManager<T> : SingletonMonoBehaviour<T> where T: SceneManager<T>
    {
        public void PushScene<S>(IParam param = default) where S : IScene
        {
            PushSceneAsync<S>(param).Forget();
        }

        public void PopScene(IParam param = null)
        {
            PopSceneAsync(param).Forget();
        }

        public void ReplaceScene<S>(IParam param = null) where S : IScene
        {
            ReplaceSceneAsync<S>(param).Forget();
        }

        public void ResetToScene<TScene>(IParam param = null) where TScene : IScene
        {
            throw new System.NotImplementedException();
        }

    }
}
