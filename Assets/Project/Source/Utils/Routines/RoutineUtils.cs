using System;
using System.Collections;
using UnityEngine;

namespace Exa.UI
{
    public static class RoutineUtils
    {
        public static IEnumerator Delay(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);

            callback();
        }
    }
}