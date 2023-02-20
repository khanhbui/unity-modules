using Cysharp.Threading.Tasks;

namespace Snakat.Scene
{
    public interface IScene
    {
        UniTask Initialize();
    }
}
