﻿using System;
using UnityEngine;

namespace Exa.Utils
{
    public static class CoroutineUtils
    {
        public static Coroutine Delay(this MonoBehaviour monoBehaviour, Action action, float seconds) {
            return Systems.Instance.StartCoroutine(EnumeratorUtils.Delay(action, seconds));
        }

        public static Coroutine DelayLocally(this MonoBehaviour monoBehaviour, Action action, float seconds) {
            return monoBehaviour.StartCoroutine(EnumeratorUtils.Delay(action, seconds));
        }
    }
}