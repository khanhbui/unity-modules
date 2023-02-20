using Cysharp.Threading.Tasks;
using Snakat.Scene;

namespace Sample
{
    public class SampleScene : Scene
    {
        void Start()
        {
            SceneManager.Instance.PushScene<SampleScene>(null);
        }

        public override UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
    }
}
