using System;

namespace Exa.Utils
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Type constrained version of <see cref="Enum.HasFlag(Enum)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool Is<T>(this T value, T flag)
            where T : Enum {
            return value.HasFlag(flag);
        }
    }
}