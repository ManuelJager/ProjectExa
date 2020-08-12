using UnityEngine;

namespace Exa.Math
{
    public static class QuaternionExtensions
    {
        public static Quaternion Multiply(this Quaternion quaternion, float scalar)
        {
            return new Quaternion((float)((double)quaternion.x * (double)scalar),
                                  (float)((double)quaternion.y * (double)scalar),
                                  (float)((double)quaternion.z * (double)scalar),
                                  (float)((double)quaternion.w * (double)scalar));
        }

        public static Quaternion RequiredRotation(Quaternion from, Quaternion to)
        {
            var requiredRotation = to * Quaternion.Inverse(from);

            // Flip the sign if w is negative.
            // This makes sure we always rotate the shortest angle to match the desired rotation.
            if (requiredRotation.w < 0.0f)
            {
                requiredRotation.x *= -1.0f;
                requiredRotation.y *= -1.0f;
                requiredRotation.z *= -1.0f;
                requiredRotation.w *= -1.0f;
            }

            return requiredRotation;
        }

        public static Quaternion Subtract(this Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion((float)((double)lhs.x - (double)rhs.x),
                                  (float)((double)lhs.y - (double)rhs.y),
                                  (float)((double)lhs.z - (double)rhs.z),
                                  (float)((double)lhs.w - (double)rhs.w));
        }
    }
}