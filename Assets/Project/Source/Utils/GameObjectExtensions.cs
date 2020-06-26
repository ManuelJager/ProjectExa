using System;
using UnityEngine;

namespace Exa.Utils
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Safely try to set the active state of a game object
        /// </summary>
        /// <param name="gameObject">Game object to setActive on</param>
        /// <param name="value">Value</param>
        public static void TrySetActive(this GameObject gameObject, bool value)
        {
            try
            {
                if (gameObject == null) return;

                gameObject.SetActive(value);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(e.Message);
            }
        }
    }
}