using System;

namespace Exa.Generics
{
    /// <summary>
    /// Used to ignore a property for descriptor
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IgnoreDescriptorBuilderAttribute : Attribute
    {
        public IgnoreDescriptorBuilderAttribute()
        {
        }
    }
}