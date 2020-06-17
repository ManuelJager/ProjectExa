using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Exa.Generics
{
    public struct MinMax<T>
    {
        public T min;
        public T max;

        public MinMax(T min, T max)
        {
            this.min = min;
            this.max = max;
        }
    }
}