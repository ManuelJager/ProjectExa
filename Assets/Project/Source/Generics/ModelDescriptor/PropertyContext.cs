using System.Reflection;

namespace Exa.Generics
{
    public struct PropertyContext
    {
        public ControlType controlType;
        public PropertyInfo propertyInfo;
        public IValuesSourceProvider sourceProvider;
    }
}