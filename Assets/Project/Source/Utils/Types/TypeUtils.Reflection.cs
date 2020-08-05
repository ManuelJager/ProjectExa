using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Exa.Utils
{
    public static partial class TypeUtils
    {
        private static Dictionary<Assembly, List<Type>> ImplementationsByAssembly = new Dictionary<Assembly, List<Type>>();

        public static IEnumerable<Func<TObject, TMember>> GetPropertyGetters<TObject, TMember>(Type objectType = null)
        {
            if (objectType == null)
            {
                objectType = typeof(TObject);
            }
            else
            {
                if (!typeof(TObject).IsAssignableFrom(objectType))
                {
                    throw new Exception("Type mismatch");
                }
            }

            var memberType = typeof(TMember);

            foreach (var propertyInfo in objectType.GetProperties())
            {
                if (memberType.IsAssignableFrom(propertyInfo.PropertyType))
                {
                    var input = Expression.Parameter(typeof(object), "input");
                    var getMethod = propertyInfo.GetGetMethod();
                    var instance = Expression.Convert(input, objectType);
                    var methodCall = Expression.Call(instance, getMethod);
                    yield return Expression.Lambda<Func<TObject, TMember>>(methodCall, input).Compile();
                }
            }
        }

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

        public static bool TryGetAttribute<T>(this MemberInfo memberInfo, out T result)
            where T : Attribute
        {
            try
            {
                result = memberInfo.GetCustomAttribute<T>();
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}