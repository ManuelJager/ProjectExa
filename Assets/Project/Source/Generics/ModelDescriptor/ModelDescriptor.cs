using Exa.Utils;
using System;
using System.Collections.Generic;

namespace Exa.Generics
{
    public abstract class ModelDescriptor<TModel> : ModelDescriptor
    {
        public ModelDescriptor()
            : base()
        {
            var type = GetType();

            if (!PropertyBindings.ContainsKey(type))
            {
                PropertyBindings[type] = new List<PropertyContext>(BuildPropertyInfoCacheCollection(type));
            }
        }

        public abstract TModel FromDescriptor();

        private IEnumerable<PropertyContext> BuildPropertyInfoCacheCollection(Type type)
        {
            foreach (var property in type.GetProperties())
            {
                ControlType controlType;

                try
                {
                    var attribute = property.GetAttribute<IgnoreDescriptorBuilderAttribute>();
                    continue;
                }
                catch { }

                if (!(property.CanRead && property.CanWrite))
                {
                    UnityEngine.Debug.LogWarning($"{property.Name} must be both read and write");
                    continue;
                }

                if (property.PropertyType != typeof(string))
                {
                    UnityEngine.Debug.LogWarning($"{property.Name} is not of type string. Types inheriting from ModelDescriptor should contain non-string properties");
                }

                SourceAttribute sourceAttribute = null;

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