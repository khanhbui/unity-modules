using System;
using UnityEngine;

namespace Snakat.Common
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour, IDisposable where T : SingletonMonoBehaviour<T>
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance ??= CreateInstance(); }
        }

        public static T CreateInstance()
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    var go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
            }

            return _instance;
        }

        public static void DeleteInstance(bool immediate = false)
        {
            if (_instance == null)
            {
                return;
            }

            if (immediate)
            {
                DestroyImmediate(_instance.gameObject);
            }
            else
            {
                Destroy(_instance.gameObject);
            }
            _instance = null;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
                _instance.Inilialize();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public virtual void Inilialize()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}
