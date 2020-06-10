using System;

namespace Exa.Generics
{
    public class SourceAttribute : Attribute
    {
        public IValuesSourceProvider Getter { get; private set; }

        public SourceAttribute(Type getter)
        {
            if (typeof(IValuesSourceProvider).IsAssignableFrom(getter))
            {
                Getter = (IValuesSourceProvider)Activator.CreateInstance(getter);
            }
            else
            {
                throw new ArgumentException($"{getter.Name} should implement the IValuesSourceProvider interface");
            }
        }
    }
}