using System.Collections.Generic;

namespace Exa.Generics
{
    public interface IDataSourceProvider
    {
        IEnumerable<ValueContext> GetValues();
    }
}