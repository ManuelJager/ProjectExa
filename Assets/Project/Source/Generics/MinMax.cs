using System;
using Exa.Math;

namespace Exa.Generics
{
    [Serializable]
    public struct MinMax<T>
    {
        public T min;
        public T max;

        public MinMax(T min, T max)
        {
            this.min = min;
            this.max = max;
        }

        public static MinMax<float> ZeroOne
        {
            get => new MinMax<float>(0f, 1f);
        }
    }

    // TODO: Remove this when upgrading to 2020.1
    [Serializable]
    public struct MinMax
    {
        public float min;
        public float max;

        public MinMax(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public static MinMax ZeroOne
        {
            get => new MinMax(0f, 1f);
        }
    }

    public static class MinMaxHelpers
    {
        public static float Evaluate(this MinMax minMax, float t)
        {
            var diff = minMax.max - minMax.min;
            return minMax.min + diff * t;
        }
    }
}