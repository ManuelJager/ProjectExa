using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Exa.Utils
{
    public static class MonoSingletonUtils
    {
        private class MonoInstanceCreationEvent : UnityEvent<object>
        {
        }

        private static Dictionary<Type, MonoInstanceCreationEvent> events = new Dictionary<Type, MonoInstanceCreationEvent>();

        /// <summary>
        /// Adds a listener that is hooked to the singleton instance creation event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public static void OnInstanceCreated<T>(UnityAction<T> action)
            where T : MonoSingleton<T>
        {
            var singletonType = typeof(T);
            if (!events.ContainsKey(singletonType))
            {
                events.Add(singletonType, new MonoInstanceCreationEvent());
            }

            // Add wrapper because UnityActions are not covariant
            events[singletonType].AddListener((obj) =>
            {
                action((T)obj);
            });
        }

        /// <summary>
        /// Used by singletons to notify listeners
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="singleton"></param>
        public static void NotifyCreated<T>(MonoSingleton singleton)
            where T : MonoSingleton<T>
        {
            var singletonType = typeof(T);
            if (!events.ContainsKey(singletonType)) return;

            events[singletonType].Invoke((T)singleton);
        }
    }
}