using System;
using System.Collections;
using System.Collections.Generic;

namespace Exa.Bindings
{
    /// <summary>
    /// Base class for virtual collection that notifies observers of model changes
    /// <para>
    /// This is preferable to just an observable<List<TData>> because it only needs to refresh views that are changed
    /// </para>
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    [Serializable]
    public class ObservableCollection<TData> : IObservableCollection<TData>
    {
        public List<TData> collection;
        public List<ICollectionObserver<TData>> Observers { get; } = new List<ICollectionObserver<TData>>();

        public ObservableCollection()
        {
            this.collection = new List<TData>();
        }

        public ObservableCollection(List<TData> collection)
        {
            this.collection = collection;
        }

        #region IList<TData> implementation

        public TData this[int index]
        {
            get => collection[index];
            set
            {
                collection[index] = value;

                foreach (var observer in Observers)
                {
                    observer.OnSet(index, value);
                }
            }
        }

        public int Count => collection.Count;

        public bool IsReadOnly => false;

        public virtual void Add(TData item)
        {
            collection.Add(item);

            foreach (var observer in Observers)
            {
                observer.OnAdd(item);
            }
        }

        public virtual void AddRange(IEnumerable<TData> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public virtual void Clear()
        {
            collection.Clear();

            foreach (var observer in Observers)
            {
                observer.OnClear();
            }
        }

        public virtual bool Contains(TData item)
        {
            return collection.Contains(item);
        }

        public virtual void CopyTo(TData[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<TData> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public virtual int IndexOf(TData item)
        {
            return collection.IndexOf(item);
        }

        public virtual void Insert(int index, TData item)
        {
            collection.Insert(index, item);

            foreach (var observer in Observers)
            {
                observer.OnInsert(index, item);
            }
        }

        public virtual bool Remove(TData item)
        {
            var removed = collection.Remove(item);

            if (removed)
            {
                foreach (var observer in Observers)
                {
                    observer.OnRemove(item);
                }
            }

            return removed;
        }

        public virtual void RemoveAt(int index)
        {
            collection.RemoveAt(index);

            foreach (var observer in Observers)
            {
                observer.OnRemoveAt(index);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion IList<TData> implementation

        /// <summary>
        /// Add an observer to the observer collection
        /// </summary>
        /// <param name="observer"></param>
        public virtual void Register(ICollectionObserver<TData> observer)
        {
            if (Observers.Contains(observer)) return;

            Observers.Add(observer);
            observer.Source = this;
        }

        /// <summary>
        /// Remove an observer from the observer collection
        /// </summary>
        /// <param name="observer"></param>
        public virtual void Unregister(ICollectionObserver<TData> observer)
        {
            if (!Observers.Contains(observer)) return;

            Observers.Remove(observer);
            observer.Source = null;
        }
    }
}