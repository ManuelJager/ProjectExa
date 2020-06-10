using UnityEngine;

namespace Exa.Utils
{
    public class MonoBehaviourInstance<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        protected static T instance = null;

        public static T Instance
        {
            set
            {
                if (instance == null)
                {
                    instance = value;
                    return;
                }
                UnityEngine.Debug.LogWarning($"Instance value has already been set on type {typeof(T)}");
            }
            get
            {
                if (instance == null)
                {
                    UnityEngine.Debug.LogError($"Missing instance on type {typeof(T)}");
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                Instance = GetComponent<T>();
            }
        }
    }
}