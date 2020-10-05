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
    public struct FloatMinMax
    {
        public float min;
        public float max;

        public FloatMinMax(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public static FloatMinMax ZeroOne
        {
            get => new FloatMinMax(0f, 1f);
        }
    }

    public static class MinMaxHelpers
    {
        public static float Evaluate(this FloatMinMax floatMinMax, float t)
        {
            var diff = floatMinMax.max - floatMinMax.min;
            return floatMinMax.min + diff * t;
        }
    }
}