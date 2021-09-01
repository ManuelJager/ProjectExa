using UnityEngine;

namespace Exa.Math {
    public static partial class MathUtils {
        public static Vector2 ToVector2(this float value) {
            return new Vector2(value, value);
        }
        
        public static string ToPercentageString(this float value) {
            return $"{value * 100f}%";
        }

        public static int DivRem(this float value, float mod, out float rem) {
            rem = value % mod;

            return Mathf.FloorToInt(value / mod);
        }
    }
}