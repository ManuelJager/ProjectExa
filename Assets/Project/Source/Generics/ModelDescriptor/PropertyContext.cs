using System.Reflection;

namespace Exa.Generics
{
    /// <summary>
    /// Container for a model descriptor property
    /// </summary>
    public struct PropertyContext
    {
        public ControlType controlType;
        public PropertyInfo propertyInfo;
        public SourceAttribute sourceAttribute;
    }
}