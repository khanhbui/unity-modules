using System.Collections.Generic;
using UnityEngine;

namespace Snakat.Scene
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        private readonly Stack<IScene> _sceneStack = new Stack<IScene>();


        private void Awake()
        {
            Instance = this;
        }

        public void PushScene<T>(IParam param = null) where T : IScene
        {
            throw new System.NotImplementedException();
        }

        public void PopScene<T>(IParam param = null) where T : IScene
        {
            throw new System.NotImplementedException();
        }

        public void ReplaceScene<T>(IParam param = null) where T : IScene
        {
            throw new System.NotImplementedException();
        }

        public void ResetToScene<T>(IParam param = null) where T : IScene
        {
            throw new System.NotImplementedException();
        }
    }
}
