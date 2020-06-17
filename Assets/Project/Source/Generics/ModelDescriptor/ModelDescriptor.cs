using Exa.Utils;
using System;
using System.Collections.Generic;

namespace Exa.Generics
{
    /// <summary>
    /// Object that contains the active string values used by a generated form
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ModelDescriptor<TModel> : ModelDescriptor
    {
        public ModelDescriptor()
            : base()
        {
            var type = GetType();

            // Ensure property context cache is only built once per type
            if (!PropertyBindings.ContainsKey(type))
            {
                PropertyBindings[type] = new List<PropertyContext>(BuildPropertyContextCacheCollection(type));
            }
        }

        public abstract TModel FromDescriptor();

        /// <summary>
        /// Build a collection of property context foreach property in the descriptor as a cache
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEnumerable<PropertyContext> BuildPropertyContextCacheCollection(Type type)
        {
            foreach (var property in type.GetProperties())
            {
                ControlType controlType;

                // Ignore property if it contains the ignore builder attribute
                try
                {
                    var attribute = property.GetAttribute<IgnoreDescriptorBuilderAttribute>();
                    continue;
                }
                catch { }

                // Ensure property can be read and written to
                if (!(property.CanRead && property.CanWrite))
                {
                    UnityEngine.Debug.LogWarning($"{property.Name} must be both read and write");
                    continue;
                }

                // Ensure property is string type
                if (property.PropertyType != typeof(string))
                {
                    UnityEngine.Debug.LogWarning($"{property.Name} is not of type string. Types inheriting from ModelDescriptor should contain non-string properties");
                }

                SourceAttribute sourceAttribute = null;

                // Test for the property having an attribute that defines the value range
                try
                {
                    sourceAttribute = property.GetAttribute<SourceAttribute>();
                    controlType = ControlType.dropdown;
                }
                catch
                {
                    controlType = ControlType.inputField;
                }

                yield return new PropertyContext
                {
                    controlType = controlType,
                    propertyInfo = property,
                    sourceAttribute = sourceAttribute
                };
            }
        }
    }

    public abstract class ModelDescriptor
    {
        [IgnoreDescriptorBuilder]
        public static Dictionary<Type, List<PropertyContext>> PropertyBindings { get; set; } = new Dictionary<Type, List<PropertyContext>>();

        [IgnoreDescriptorBuilder]
        public Type DescriptorType { get; }


        public ModelDescriptor()
        {
            this.DescriptorType = GetType();
        }

        public List<PropertyContext> GetProperties()
        {
            return PropertyBindings[DescriptorType];
        }
    }
}