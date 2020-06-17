using Exa;
using System;

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
    }
}