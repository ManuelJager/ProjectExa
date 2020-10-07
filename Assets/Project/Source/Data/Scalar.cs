using System;
using UnityEngine;

namespace Exa.Data
{
    [Serializable]
    public struct Scalar : IFormattable
    {
        [SerializeField] private float _scalar;

        public Scalar(float value)
        {
            this._scalar = value;
        }

        public float GetValue(float real)
        {
            return real * _scalar;
        }

        public Vector2 GetValue(Vector2 real)
        {
            return real * _scalar;
        }

        public static bool operator >(Scalar a, Scalar b)
        {
            return a._scalar > b._scalar;
        }

        public static bool operator <(Scalar a, Scalar b)
        {
            return a._scalar < b._scalar;
        }

        public static Scalar operator -(Scalar a, Scalar b)
        {
            return new Scalar(a._scalar - b._scalar);
        }

        public static Scalar operator +(Scalar a, Scalar b)
        {
            return new Scalar(a._scalar + b._scalar);
        }

        public string ToPercentageString()
        {
            return $"{_scalar * 100f}%";
        }

        public override string ToString()
        {
            return _scalar.ToString();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _scalar.ToString(format, formatProvider);
        }
    }
}