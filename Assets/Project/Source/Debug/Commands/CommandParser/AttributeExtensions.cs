using System;

namespace Exa.Debugging.Commands.Parser
{
    internal static class AttributeExtensions
    {
        /// <summary>
        /// Gets the first Attribute of given type in the given property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static T GetProperty<T>(this System.Reflection.PropertyInfo propertyInfo)
            where T : Attribute
        {
            try
            {
                var def = (T)propertyInfo.GetCustomAttributes(typeof(T), false)[0];
                return def;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException($"Attribute {nameof(T)} not found on property {propertyInfo.Name} on object {propertyInfo.DeclaringType.Name}");
            }
        }
    }
}