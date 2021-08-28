using System;

namespace Exa.Utils {
    public static class NullableExtensions {
        public static bool GetHasValue<T>(this T? nullable, out T value) 
            where T : struct {
            value = nullable.GetValueOrDefault();

            return nullable.HasValue;
        }
    }
}