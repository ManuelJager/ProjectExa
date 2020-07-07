using System.Collections.Generic;
using UnityEngine;

namespace Exa.Generics
{
    /// <summary>
    /// Supports a collection of possible values for a model descriptor property
    /// </summary>
    public interface IDataSourceProvider
    {
        IEnumerable<NamedWrapper<object>> GetValues();

        void OnOptionCreation(object value, GameObject viewObject);
    }
}