using System.Collections.Generic;

namespace Exa.Generics
{
    /// <summary>
    /// Supports a collection of possible values for a model descriptor property
    /// </summary>
    public interface IDataSourceProvider
    {
        IEnumerable<ValueContext> GetValues();
    }
}