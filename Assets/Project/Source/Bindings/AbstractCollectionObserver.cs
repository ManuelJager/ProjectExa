using System;
using UnityEngine;

namespace Exa.Bindings
{
    public abstract class AbstractCollectionObserver<T> : MonoBehaviour, ICollectionObserver<T>
    {
        private IObservableCollection<T> _source = null;

        /// <summary>
        /// Views data source
        /// </summary>
        public virtual IObservableCollection<T> Source
        {
            get => _source;
            set
            {
                // If the new source is the same, do nothing
                if (_source == value)
                {
                    return;
                }

                // If there is already a source, unregister
                if (_source != null)
                {
                    // Unregistered if we are registered
                    if (_source.Observers.Contains(this))
                    {
                        _source.Unregister(this);
                    }
                    _source = null;
                }

                // if the new value is supossed to be null, clear views and return
                if (value == null)
                {
                    OnClear();
                    return;
                }

                // Set source and register
                _source = value;
                _source.Register(this);

                // Clear views
                OnClear();

                foreach (var item in _source)
                {
                    OnAdd(item);
                }
            }
        }

        public abstract void OnAdd(T data);

        public abstract void OnClear();

        public abstract void OnRemove(T data);
    }
}