using System.Collections.Generic;
using UnityEngine;

namespace Exa.Utils
{
    public static class VectorHelpers
    {
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

        public static IEnumerable<Vector2Int> EnumerateVectors(Vector2Int size)
        {
            return EnumerateVectors(size.x, size.y);
        }

        public static IEnumerable<Vector2Int> EnumerateVectors(Vector2Int size, Vector2Int offset)
        {
            return EnumerateVectors(size.x, size.y, offset.x, offset.y);
        }

        public static Vector3 ToVector3(this Vector3Int from)
        {
            return new Vector3(from.x, from.y, from.z);
        }

        public static Vector3 ToVector3(this Vector2Int from, float z = 0f)
        {
            return new Vector3(from.x, from.y, z);
        }

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