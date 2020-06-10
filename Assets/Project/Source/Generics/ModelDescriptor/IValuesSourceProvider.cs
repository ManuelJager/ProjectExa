using System.Collections.Generic;

namespace Exa.Generics
{
    public interface IValuesSourceProvider
    {
        IEnumerable<ValueContext> GetValues();
    }
}