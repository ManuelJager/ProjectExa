using System;
using UnityEngine;

namespace Exa.Bindings
{
    public abstract class AbstractCollectionObserver<T> : MonoBehaviour, ICollectionObserver<T>
    {
        private IObservableCollection<T> source = null;

        /// <summary>
        /// Views data source
        /// </summary>
        public virtual IObservableCollection<T> Source
        {
            get => source;
            set
            {
                // If the new source is the same, do nothing
                if (source == value)
                {
                    return;
                }

                // If there is already a source, unregister
                if (source != null)
                {
                    // Unregistered if we are registered
                    if (source.Observers.Contains(this))
                    {
                        source.Unregister(this);
                    }
                    source = null;
                }

                // if the new value is supossed to be null, clear views and return
                if (value == null)
                {
                    OnClear();
                    return;
                }

                // Set source and register
                source = value;
                source.Register(this);

                // Clear views
                OnClear();

                foreach (var item in source)
                {
                    OnAdd(item);
                }
            }
        }

        public abstract void OnAdd(T data);

        public abstract void OnClear();

        public virtual void OnInsert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public abstract void OnRemove(T data);

        public virtual void OnRemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public virtual void OnSet(int index, T item)
        {
            throw new NotImplementedException();
        }
    }
}