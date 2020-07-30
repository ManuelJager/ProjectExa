using System;

namespace UCommandConsole
{
    internal class ParameterContext : IParameterContext
    {
        public Type PropertyType { get; private set; }
        public Type CustomParser { get; set; }
        public bool Required { get; set; }
        public string Name { get; set; }

        public ParameterContext(Type propertyType)
        {
            this.PropertyType = propertyType;
        }
    }
}