using System.Collections;
using System.Collections.Generic;

namespace Exa.Bindings
{
    /// <summary>
    /// Provides
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObservableCollection<T> :
        ICollection<T>,
        IEnumerable<T>,
        IEnumerable,
        IList<T>,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>
    {
        List<ICollectionObserver<T>> Observers { get; }

        void Register(ICollectionObserver<T> observer);

        void Unregister(ICollectionObserver<T> observer);
    }
}