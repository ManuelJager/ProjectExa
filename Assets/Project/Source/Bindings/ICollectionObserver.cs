namespace Exa.Bindings
{
    /// <summary>
    /// Interface used by <see cref="IObservableCollection{TData}"/> to add and remove models from a view controller
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public interface ICollectionObserver<T>
    {
        /// <summary>
        /// Data source used by the observer
        /// </summary>
        IObservableCollection<T> Source { get; set; }

        /// <summary>
        /// On replace model
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        void OnSet(int index, T item);

        /// <summary>
        /// On add model
        /// </summary>
        /// <param name="data"></param>
        void OnAdd(T data);

        /// <summary>
        /// On clear all models
        /// </summary>
        void OnClear();

        /// <summary>
        /// On insert a model in the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        void OnInsert(int index, T item);

        /// <summary>
        /// On remove a model
        /// </summary>
        /// <param name="data"></param>
        void OnRemove(T data);

        /// <summary>
        /// On remove a model in the specified index
        /// </summary>
        /// <param name="index"></param>
        void OnRemoveAt(int index);
    }
}