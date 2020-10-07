using System;
using UnityEngine;

namespace Exa.Data
{
    [Serializable]
    public struct Scalar : IFormattable
    {
        [SerializeField] private float scalar;

        public Scalar(float value)
        {
            this.scalar = value;
        }

        public float GetValue(float real)
        {
            return real * scalar;
        }

        public Vector2 GetValue(Vector2 real)
        {
            return real * scalar;
        }

        public static bool operator >(Scalar a, Scalar b)
        {
            return a.scalar > b.scalar;
        }

        public static bool operator <(Scalar a, Scalar b)
        {
            return a.scalar < b.scalar;
        }

        public static Scalar operator -(Scalar a, Scalar b)
        {
            return new Scalar(a.scalar - b.scalar);
        }

        public static Scalar operator +(Scalar a, Scalar b)
        {
            return new Scalar(a.scalar + b.scalar);
        }

        public string ToPercentageString()
        {
            return $"{scalar * 100f}%";
        }

        public override string ToString()
        {
            return scalar.ToString();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return scalar.ToString(format, formatProvider);
        }
    }
}