using UnityEngine;

namespace Service.Singleton
{
    public abstract class AbstractSingleton : MonoBehaviour, ISingularObject
    {
        protected abstract void MakeCurrent();
        protected AbstractSingleton() { }
        
        void ISingularObject.Initialize()
        {
            MakeCurrent();
        }
    }
    
    public abstract class AbstractSingleton<T> : AbstractSingleton where T : AbstractSingleton<T>
    {
        public static T Instance { get; private set; }

        protected sealed override void MakeCurrent()
        {
            if (!Instance)
            {
                Instance = (T)this;
            }
            else
            {
                Destroy(this);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}