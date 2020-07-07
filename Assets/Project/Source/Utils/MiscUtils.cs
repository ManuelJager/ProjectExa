using System;
using UnityEngine;

namespace Exa.Utils
{
    public static class MiscUtils
    {
        /// <summary>
        /// Wrapper method for invoking an action if the application is not currently quitting
        /// <para>
        /// This is used to prevent unnecessary and potentially unsafe cleanup calls during application exit
        /// </para>
        /// </summary>
        /// <param name="action"></param>
        public static void InvokeIfNotQuitting(Action action)
        {
            if (MainManager.IsQuitting) return;

            action();
        }

        public static T[] GetAllInstances<T>()
            where T : ScriptableObject
        {
            return Resources.FindObjectsOfTypeAll(typeof(T)) as T[];
        }

        public static T[] GetAllInstances<T>(string path)
            where T : ScriptableObject
        {
            return Resources.LoadAll<T>(path);
        }
    }
}