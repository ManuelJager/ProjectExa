using System;
using UnityEngine;

namespace Exa.Data
{
    [Serializable]
    public struct Percentage
    {
        [SerializeField] private float ratio;

        public Percentage(float value)
        {
            this.ratio = value;
        }

        public float GetValue(float real)
        {
            return real * ratio;
        }

        public static bool operator >(Percentage a, Percentage b)
        {
            return a.ratio > b.ratio;
        }

        public static bool operator <(Percentage a, Percentage b)
        {
            return a.ratio < b.ratio;
        }

        public static Percentage operator -(Percentage a, Percentage b)
        {
            return new Percentage(a.ratio - b.ratio);
        }

        public static Percentage operator +(Percentage a, Percentage b)
        {
            return new Percentage(a.ratio + b.ratio);
        }

        public override string ToString()
        {
            return $"{ratio * 100f}%";
        }
    }
}