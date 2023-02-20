using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Snakat.Scene
{
    public abstract class Scene : MonoBehaviour, IScene
    {
        public abstract UniTask Initialize();
    }
}
