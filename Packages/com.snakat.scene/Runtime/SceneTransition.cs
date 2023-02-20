using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Snakat.Scene
{
    public abstract class SceneTransition : MonoBehaviour, ISceneTransition
    {
        public abstract UniTask TransitonIn();
        public abstract UniTask TransitonOut();
    }
}
