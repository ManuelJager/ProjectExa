﻿using System;
using UnityEngine;

namespace Exa.Generics
{
    [Serializable]
    public struct MinMax<T>
    {
        public T min;
        public T max;

        public MinMax(T min, T max) {
            this.min = min;
            this.max = max;
        }

        public static MinMax<float> ZeroOne => new MinMax<float>(0f, 1f);
    }

    public static class MinMaxHelpers
    {
        public static float Evaluate(this MinMax<float> minMax, float t) {
            var diff = minMax.max - minMax.min;
            return minMax.min + diff * t;
        }

        public static float Clamp(this MinMax<float> minMax, float value) {
            return Mathf.Clamp(value, minMax.min, minMax.max);
        }
    }
}