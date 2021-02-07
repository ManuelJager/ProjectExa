using System;
using System.Collections;
using UnityEngine;

namespace Exa.Utils
{
    public static class CoroutineUtils
    {
        public static Coroutine Delay(this MonoBehaviour monoBehaviour, Action action, float seconds) {
            return EnumeratorUtils.Delay(action, seconds).Start();
        }

        public static Coroutine Start(this IEnumerator enumerator) {
            return Systems.Instance.StartCoroutine(enumerator);
        }

        public static Coroutine DelayLocally(this MonoBehaviour monoBehaviour, Action action, float seconds) {
            return monoBehaviour.StartCoroutine(EnumeratorUtils.Delay(action, seconds));
        }
    }
}