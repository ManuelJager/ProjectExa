using System;

namespace Exa.Utils
{
    public static partial class TypeUtils
    {
        /// <summary>
        /// Runs the callback when the given object is of type T
        /// </summary>
        /// <typeparam name="T">Type to checks</typeparam>
        /// <param name="object">parameter</param>
        /// <param name="action">object callback</param>
        /// <returns>wether object is assignable from T</returns>
        public static bool OnAssignableFrom<T>(this object @object, Action<T> action)
        {
            if (typeof(T).IsAssignableFrom(@object.GetType()))
            {
                action((T)@object);
                return true;
            }
            return false;
        }
    }
}