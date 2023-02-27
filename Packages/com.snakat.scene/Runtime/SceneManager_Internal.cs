using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Snakat.Common;
using Snakat.Common.Attribute;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snakat.Scene
{
    public partial class SceneManager<T> : SingletonMonoBehaviour<T> where T : SceneManager<T>
    {
        private readonly Stack<IScene> _sceneStack = new();
        private IScene _currentScene = null;
        private ISceneTransition _sceneTransition = null;

        private bool _isTransition = false;


        private async UniTask PushSceneAsync<S>(IParam param) where S : IScene
        {
            _isTransition = true;

            if (_currentScene != null)
            {
                await _currentScene.OnBeginTransitionOut();
                if (_sceneTransition != null)
                {
                    await _sceneTransition.TransitonIn();
                }
                await _currentScene.OnEndTransitionOut();
                await _currentScene.OnPause();
            }

            await LoadScene<S>();

            if (_currentScene != null)
            {
                await _currentScene.OnInitialize(param);
                await _currentScene.OnBeginTransitionIn(param);
                if (_sceneTransition != null)
                {
                    await _sceneTransition.TransitonOut();
                }
                await _currentScene.OnEndTransitionIn(param);
                await _currentScene.OnEnter(param);

                _sceneStack.Push(_currentScene);
            }

            _isTransition = false;
        }

        private async UniTask PopSceneAsync(IParam param)
        {
            if (_isTransition)
            {
                return;
            }
            if (_sceneStack.Count <= 1)
            {
                return;
            }

            _isTransition = true;

            if (_currentScene != null)
            {
                await _currentScene.OnBeginTransitionOut();
                if (_sceneTransition != null)
                {
                    await _sceneTransition.TransitonIn();
                }
                await _currentScene.OnEndTransitionOut();
                await _currentScene.OnExit();

                await UnloadScene(_currentScene.GetType());

                _sceneStack.Pop();
            }

            _currentScene = _sceneStack.Peek();
            if (_currentScene != null)
            {
                await _currentScene.OnBeginTransitionIn(param);
                if (_sceneTransition != null)
                {
                    await _sceneTransition.TransitonOut();
                }
                await _currentScene.OnEndTransitionIn(param);
                await _currentScene.OnResume(param);
            }

            _isTransition = false;
        }

        private async UniTask ReplaceSceneAsync<S>(IParam param) where S : IScene
        {
            if (_isTransition)
            {
                return;
            }
            if (_sceneStack.Count == 0)
            {
                return;
            }
            
            _isTransition = true;

            if (_currentScene != null)
            {
                await _currentScene.OnBeginTransitionOut();
                if (_sceneTransition != null)
                {
                    await _sceneTransition.TransitonIn();
                }
                await _currentScene.OnEndTransitionOut();
                await _currentScene.OnExit();

                await UnloadScene(_currentScene.GetType());

                _sceneStack.Pop();
            }

            await LoadScene<S>();

            if (_currentScene != null)
            {
                await _currentScene.OnInitialize(param);
                await _currentScene.OnBeginTransitionIn(param);
                if (_sceneTransition != null)
                {
                    await _sceneTransition.TransitonOut();
                }
                await _currentScene.OnEndTransitionIn(param);
                await _currentScene.OnEnter(param);

                _sceneStack.Push(_currentScene);
            }

            _isTransition = false;
        }

        #region Load Scene

        private async UniTask LoadScene<S>() where S : IScene
        {
            _currentScene = null;
            SceneManager.sceneLoaded += OnSceneLoaded<S>;

            var attr = Attribute.GetCustomAttribute(typeof(S), typeof(FileAttribute)) as FileAttribute;
            await SceneManager.LoadSceneAsync(attr.Path, LoadSceneMode.Additive);

            await UniTask.WaitUntil(() => _currentScene != null);
            SceneManager.sceneLoaded -= OnSceneLoaded<S>;

            Debug.Log($"kaka loaded _currentScene={_currentScene}");
        }

        private void OnSceneLoaded<S>(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) where S : IScene
        {
            Debug.Log($"kaka loaded {typeof(S)} scene={scene.path} mode={mode}");

            var roots = scene.GetRootGameObjects();
            foreach (var root in roots)
            {
                if (root.TryGetComponent<IScene>(out var component))
                {
                    if (component is S)
                    {
                        _currentScene = component;
                        break;
                    }
                }
            }
        }

        #endregion

        #region Unload Scene

        private async UniTask UnloadScene(Type type)
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            var attr = Attribute.GetCustomAttribute(type, typeof(FileAttribute)) as FileAttribute;
            await SceneManager.UnloadSceneAsync(attr.Path);

            await UniTask.WaitUntil(() => _currentScene == null);
            SceneManager.sceneUnloaded -= OnSceneUnloaded;

            Debug.Log($"kaka unloaded _currentScene={_currentScene}");
        }

        private void OnSceneUnloaded(UnityEngine.SceneManagement.Scene scene)
        {
            Debug.Log($"kaka unloaded scene={scene.path}");
            _currentScene = null;
        }

        #endregion

    }
}
