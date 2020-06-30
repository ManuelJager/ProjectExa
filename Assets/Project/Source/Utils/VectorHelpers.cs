using System.Collections.Generic;
using UnityEngine;

namespace Exa.Utils
{
    public static class VectorHelpers
    {
        /// <summary>
        /// Rotate a vector2int by the given count of quarter turns
        /// </summary>
        /// <param name="vector">Vector2int to be rotated</param>
        /// <param name="quarterTurns">Amount of 90 degree turns</param>
        /// <returns></returns>
        public static Vector2Int Rotate(this Vector2Int vector, int quarterTurns)
        {
            // if the vector is rotated by 360 degrees we dont need to calculate anything
            if (quarterTurns % 4 == 0) return vector;

            var sin = Mathf.Sin(quarterTurns * 90 * Mathf.Deg2Rad);
            var cos = Mathf.Cos(quarterTurns * 90 * Mathf.Deg2Rad);

            var tx = vector.x;
            var ty = vector.y;

            vector.x = Mathf.RoundToInt((cos * tx) - (sin * ty));
            vector.y = Mathf.RoundToInt((sin * tx) + (cos * ty));

            return vector;
        }

        public static string ToShortString(this Vector2Int vector)
        {
            return $"{vector.x},{vector.y}";
        }

        /// <summary>
        /// Rotate a vector2 by the given count of quarter turns
        /// </summary>
        /// <param name="vector">Vector2 to be rotated</param>
        /// <param name="quarterTurns">Amount of 90 degree turns</param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 vector, int quarterTurns)
        {
            // if the vector is rotated by 360 degrees we dont need to calculate anything
            if (quarterTurns % 4 == 0) return vector;

            var sin = Mathf.Sin(quarterTurns * 90 * Mathf.Deg2Rad);
            var cos = Mathf.Cos(quarterTurns * 90 * Mathf.Deg2Rad);

            var tx = vector.x;
            var ty = vector.y;

            vector.x = (cos * tx) - (sin * ty);
            vector.y = (sin * tx) + (cos * ty);

            return vector;
        }

        /// <summary>
        /// Enumerate through a square of vectors
        /// </summary>
        /// <param name="sizeX">Width</param>
        /// <param name="sizeY">Height</param>
        /// <returns></returns>
        public static IEnumerable<Vector2Int> EnumerateVectors(int sizeX, int sizeY)
        {
            foreach (var x in Range(sizeX))
            {
                foreach (var y in Range(sizeY))
                {
                    yield return new Vector2Int(x, y);
                }
            }
        }

        /// <summary>
        /// Enumerate through a square of vectors from the given offset
        /// </summary>
        /// <param name="sizeX">Horizontal offset</param>
        /// <param name="sizeY">Vertical offset</param>
        /// <param name="offsetX">Width</param>
        /// <param name="offsetY">Height</param>
        /// <returns></returns>
        public static IEnumerable<Vector2Int> EnumerateVectors(int sizeX, int sizeY, int offsetX, int offsetY)
        {
            foreach (var x in Range(sizeX))
            {
                foreach (var y in Range(sizeY))
                {
                    yield return new Vector2Int(x + offsetX, y + offsetY);
                }
            }
        }

        /// <inheritdoc cref="EnumerateVectors(int, int)"/>
        public static IEnumerable<Vector2Int> EnumerateVectors(Vector2Int size)
        {
            return EnumerateVectors(size.x, size.y);
        }

        /// <inheritdoc cref="EnumerateVectors(int, int, int, int)"/>
        public static IEnumerable<Vector2Int> EnumerateVectors(Vector2Int size, Vector2Int offset)
        {
            return EnumerateVectors(size.x, size.y, offset.x, offset.y);
        }

        /// <summary>
        /// Convert a vector3int to a vector
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector3Int from)
        {
            return new Vector3(from.x, from.y, from.z);
        }

        /// <summary>
        /// Convert a vector2int to a vector3 with the optional z angle
        /// </summary>
        /// <param name="from"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector2Int from, float z = 0f)
        {
            return new Vector3(from.x, from.y, z);
        }

        /// <summary>
        /// Convert a vector2 to a vector3 with the optional z angle
        /// </summary>
        /// <param name="from"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector2 from, float z = 0f)
        {
            return new Vector3(from.x, from.y, z);
        }

        public static Vector2 ToVector2(this Vector3Int from)
        {
            return new Vector2(from.x, from.y);
        }

        public static Vector2 ToVector2(this Vector2Int from)
        {
            return new Vector2(from.x, from.y);
        }

        public static Vector2 ToVector2(this Vector3 from)
        {
            return new Vector2(from.x, from.y);
        }

        /// <summary>
        /// Enumerate through a range of intergers, if the value is negative the enumeration will go backwards
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static IEnumerable<int> Range(int value)
        {
            if (value > 0)
            {
                for (int i = 0; i < value; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (int i = 0; i > value; i--)
                {
                    yield return i;
                }
            }
        }
    }
}