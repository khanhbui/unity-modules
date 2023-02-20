using Cysharp.Threading.Tasks;

namespace Snakat.Scene
{
    public interface ISceneTransition
    {
        UniTask TransitonIn();
        UniTask TransitonOut();
    }
}
