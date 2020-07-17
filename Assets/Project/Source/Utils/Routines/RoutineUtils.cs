using System;
using System.Collections;
using UnityEngine;

namespace Exa.Utils
{
    public static class RoutineUtils
    {
        public static IEnumerator DelayOneFrame(Action callback)
        {
            yield return 0;

            callback();
        }

        public static IEnumerator Delay(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);

            callback();
        }
    }
}