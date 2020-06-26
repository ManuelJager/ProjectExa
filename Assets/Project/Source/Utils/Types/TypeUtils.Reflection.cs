using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Exa.Utils
{
    public static partial class TypeUtils
    {
        private static Dictionary<Assembly, List<Type>> ImplementationsByAssembly = new Dictionary<Assembly, List<Type>>();

        /// <summary>
        /// Get types in assembly that implement the given parent type
        /// </summary>
        /// <param name="assembly">Assembly in which to query for</param>
        /// <param name="parentType">Type children must inherit from</param>
        /// <returns>List of implementations for parentType</returns>
        public static IEnumerable<Type> GetTypeImplementations(Assembly assembly, Type parentType)
        {
            if (!ImplementationsByAssembly.ContainsKey(assembly))
            {
                ImplementationsByAssembly[assembly] = new List<Type>(
                    assembly
                    .GetTypes()
                    .Where(type => type.IsClass && !type.IsAbstract));
            }

            foreach (var implementationType in ImplementationsByAssembly[assembly])
            {
                if (implementationType.IsSubclassOf(parentType))
                {
                    yield return implementationType;
                }
            }
        }

        /// <summary>
        /// Get types in current executing assembly
        /// </summary>
        /// <param name="parentType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypeImplementations(Type parentType)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return GetTypeImplementations(assembly, parentType);
        }

        /// <summary>
        /// Get attribute from property
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="propertyInfo">Property info</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this PropertyInfo propertyInfo)
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