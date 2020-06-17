using System.Collections.Generic;
using UnityEngine;

namespace Exa.Bindings
{
    /// <summary>
    /// Controls views for a collection of models
    /// <para>
    /// Handles instantiating views, binding model observables to their corresponding views, and deleting views
    /// </para>
    /// </summary>
    /// <typeparam name="TView">View type, must be an observer of <see cref="{TModel}"/></typeparam>
    /// <typeparam name="TModelObservable">Model observable type</typeparam>
    /// <typeparam name="TModel">Model type</typeparam>
    public class ViewController<TView, TModelObservable, TModel> : MonoBehaviour, ICollectionObserver<TModelObservable>
        where TView : MonoBehaviour, IObserver<TModel>
        where TModelObservable : Observable<TModel>
        where TModel : class
    {
        [SerializeField] protected Transform viewContainer;
        [SerializeField] protected GameObject viewPrefab;

        protected Dictionary<TModelObservable, TView> views = new Dictionary<TModelObservable, TView>();

        private IObservableCollection<TModelObservable> source = null;

        /// <summary>
        /// Views data source
        /// </summary>
        public virtual IObservableCollection<TModelObservable> Source
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

        /// <summary>
        /// Add Collection of observables
        /// </summary>
        /// <param name="collection"></param>
        public virtual void AddRange(IEnumerable<TModelObservable> collection)
        {
            foreach (var item in source)
            {
                OnAdd(item);
            }
        }

        /// <summary>
        /// Add observable
        /// </summary>
        /// <param name="observer"></param>
        public virtual void OnAdd(TModelObservable observer)
        {
            OnAdd(observer, viewContainer);
        }

        /// <summary>
        /// Add Observable with specified view container
        /// </summary>
        /// <param name="observer"></param>
        /// <param name="container"></param>
        protected virtual void OnAdd(TModelObservable observer, Transform container)
        {
            var blockObject = Instantiate(viewPrefab, container);
            var view = blockObject.GetComponent<TView>();
            view.OnUpdate(observer.Data);
            observer.Register(view);
            views.Add(observer, view);
            ViewCreation(view, observer);
        }
        
        /// <summary>
        /// Is called after a view is created, may be override to add custom event listeners by inheritor
        /// </summary>
        /// <param name="view"></param>
        /// <param name="observer"></param>
        public virtual void ViewCreation(TView view, TModelObservable observer)
        {
        }

        /// <summary>
        /// Clear views
        /// </summary>
        public virtual void OnClear()
        {
            foreach (var key in views.Keys)
            {
                OnRemove(key);
            }

            views = new Dictionary<TModelObservable, TView>();
        }

        public virtual void OnInsert(int index, TModelObservable observer)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Remove view
        /// </summary>
        /// <param name="observer"></param>
        public virtual void OnRemove(TModelObservable observer)
        {
            var view = views[observer];
            observer.Unregister(view);
            Destroy(view.gameObject);
        }

        public virtual void OnRemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnSet(int index, TModelObservable observer)
        {
            throw new System.NotImplementedException();
        }
    }
}