using UnityEngine;

namespace Exa.Utils
{
    public static class NullExtensions
    {
        public static bool IsNull<T>(this T input, out T output)
            where T : class {
            output = input;
            return input.IsNull();
        }

        public static bool IsNull<T>(this T input)
            where T : class {
            if (input is Object unityObject) {
                return unityObject == null;
            }

            return input == null;
        }
    }
}