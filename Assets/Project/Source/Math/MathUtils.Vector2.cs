using UnityEngine;

namespace Exa.Math
{
    public static partial class MathUtils
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

        public static Vector2 ToVector2(this Vector2Int from)
        {
            return new Vector2(from.x, from.y);
        }

        public static Vector2Int GetRatio(Vector2Int value)
        {
            var gdc = GreatestCommonDevisor(value.x, value.y);
            return new Vector2Int
            {
                x = value.x / gdc,
                y = value.y / gdc
            };
        }
    }
}