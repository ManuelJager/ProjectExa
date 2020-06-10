using System;

namespace Exa.Generics
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IgnoreDescriptorBuilderAttribute : Attribute
    {
        public IgnoreDescriptorBuilderAttribute()
        {
        }
    }
}