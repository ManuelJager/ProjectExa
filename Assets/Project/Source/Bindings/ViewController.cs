using System.Collections.Generic;
using UnityEngine;

namespace Exa.Bindings
{
    public class ViewController<TView, TModelObserver, TModel> : MonoBehaviour, ICollectionObserver<TModelObserver>
        where TView : MonoBehaviour, IObserver<TModel>
        where TModelObserver : Observable<TModel>
        where TModel : class
    {
        [SerializeField] protected Transform viewContainer;
        [SerializeField] protected GameObject viewPrefab;

        protected Dictionary<TModelObserver, TView> views = new Dictionary<TModelObserver, TView>();

        private IObservableCollection<TModelObserver> source = null;

        public virtual IObservableCollection<TModelObserver> Source
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

                AddRange(source);
            }
        }

        public virtual void AddRange(IEnumerable<TModelObserver> collection)
        {
            foreach (var item in source)
            {
                OnAdd(item);
            }
        }

        public virtual void OnAdd(TModelObserver observer)
        {
            OnAdd(observer, viewContainer);
        }

        protected virtual void OnAdd(TModelObserver observer, Transform container)
        {
            var blockObject = Instantiate(viewPrefab, container);
            var view = blockObject.GetComponent<TView>();
            view.OnUpdate(observer.Data);
            observer.Register(view);
            views.Add(observer, view);
            ViewCreation(view, observer);
        }

        public virtual void ViewCreation(TView view, TModelObserver observer)
        {
        }

        public virtual void OnClear()
        {
            foreach (var key in views.Keys)
            {
                OnRemove(key);
            }

            views = new Dictionary<TModelObserver, TView>();
        }

        public virtual void OnInsert(int index, TModelObserver observer)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnRemove(TModelObserver observer)
        {
            var view = views[observer];
            observer.Unregister(view);
            Destroy(view.gameObject);
        }

        public virtual void OnRemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnSet(int index, TModelObserver observer)
        {
            throw new System.NotImplementedException();
        }
    }
}