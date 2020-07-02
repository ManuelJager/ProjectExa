using System;
using UnityEditor;
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
            if (GameManager.IsQuitting) return;

            action();
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static T[] GetAllInstances<T>() 
            where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;

        }
    }
}