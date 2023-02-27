using System;

namespace Snakat.Common
{
    public class Singleton<T> : IDisposable where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance ??= CreateInstance(); }
        }

        public static bool HasInstance
        {
            get
            {
                return _instance != null;
            }
        }

        public static T CreateInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = (T)Activator.CreateInstance(typeof(T), true);
            _instance.Initialize();

            return _instance;
        }

        public static void DeleteInstance()
        {
            if (_instance == null)
            {
                return;
            }
            _instance = null;
        }

        protected Singleton()
        {
        }

        ~Singleton()
        {
            Dispose();
        }

        public virtual void Initialize()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}
