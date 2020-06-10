using System;
using UnityEngine;

namespace Exa.Utils
{
    public static class GameObjectExtensions
    {
        public static void TrySetActive(this GameObject gameObject, bool value)
        {
            try
            {
                if (gameObject == null) return;

                gameObject.SetActive(value);
            }
            catch(Exception e)
            {
                UnityEngine.Debug.LogWarning(e.Message);
            }
        }
    }
}