using Game.Scene.Title;

namespace Game.Core
{
    public class SceneManager : Snakat.Scene.SceneManager<SceneManager>
    {
        public override void Inilialize()
        {
            base.Inilialize();

            PushScene<TitleScene>();
        }
    }
}
