using System;
using UCommandConsole.Exceptions;

namespace UCommandConsole.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandPropertyAttribute : Attribute, IParameterInfo
    {
        public Type CustomParser { get; }

        public bool Required => false;

        public CommandPropertyAttribute()
        {
            this.CustomParser = null;
        }

        public CommandPropertyAttribute(Type customParserType)
        {
            if (!typeof(TypeParser).IsAssignableFrom(customParserType))
            {
                throw new TypeMismatchException(typeof(TypeParser), customParserType);
            }

            this.CustomParser = customParserType;
        }
    }
}