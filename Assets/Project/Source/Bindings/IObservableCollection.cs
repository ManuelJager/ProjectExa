using System.Collections.Generic;

namespace Exa.Bindings
{
    /// <summary>
    /// Provides
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObservableCollection<T> : ICollection<T>
    {
        List<ICollectionObserver<T>> Observers { get; }

        void Register(ICollectionObserver<T> observer);

        void Unregister(ICollectionObserver<T> observer);
    }
}