using System;
using UnityEngine;

namespace Exa.Utils
{
    public class MonoSingleton<T> : MonoSingleton
        where T : MonoSingleton<T>
    {
        protected static T instance = null;

        /// <summary>
        /// Static reference that can only be set once internally
        /// </summary>
        public static T Instance
        {
            private set
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
                    throw new NullReferenceException($"Missing instance on type {typeof(T)}");
                }
                return instance;
            }
        }

        /// <summary>
        /// Get the component
        /// </summary>
        protected virtual void Awake()
        {
            if (instance == null)
            {
                Instance = GetComponent<T>();
            }
        }
    }

    public abstract class MonoSingleton : MonoBehaviour
    {
    }
}