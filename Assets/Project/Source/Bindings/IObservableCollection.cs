using System.Collections;
using System.Collections.Generic;

namespace Exa.Bindings
{
    /// <summary>
    /// Provides 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IObservableCollection<TData> :
        ICollection<TData>,
        IEnumerable<TData>,
        IEnumerable,
        IList<TData>,
        IReadOnlyCollection<TData>,
        IReadOnlyList<TData>
    {
        List<ICollectionObserver<TData>> Observers { get; }

        void Register(ICollectionObserver<TData> observer);

        void Unregister(ICollectionObserver<TData> observer);
    }
}