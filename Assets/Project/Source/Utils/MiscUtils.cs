using System;
using UnityEngine;

namespace Exa.Utils
{
    public static class MiscUtils
    {
        public static bool IsQuitting { get; set; }

        /// <summary>
        /// Wrapper method for invoking an action if the application is not currently quitting
        /// <para>
        /// This is used to prevent unnecessary and potentially unsafe cleanup calls during application exit
        /// </para>
        /// </summary>
        /// <param name="action"></param>
        public static void InvokeIfNotQuitting(Action action)
        {
            if (IsQuitting) return;

            action();
        }

        [Obsolete("Poor reliability, use scriptable object bags instead")]
        public static T[] GetAllInstances<T>()
            where T : ScriptableObject
        {
            return Resources.FindObjectsOfTypeAll(typeof(T)) as T[];
        }

        [Obsolete("Poor scaleability, use scriptable object bags instead")]
        public static T[] GetAllInstances<T>(string path)
            where T : ScriptableObject
        {
            return Resources.LoadAll<T>(path);
        }
    }
}