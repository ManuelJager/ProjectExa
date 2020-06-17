namespace Exa.Bindings
{
    /// <summary>
    /// Interface used by <see cref="IObservableCollection{TData}"/> to add and remove models from a view controller
    /// </summary>
    /// <typeparam name="TData">Model type</typeparam>
    public interface ICollectionObserver<TData>
    {
        /// <summary>
        /// Data source used by the observer
        /// </summary>
        IObservableCollection<TData> Source { get; set; }

        /// <summary>
        /// On replace model
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        void OnSet(int index, TData item);

        /// <summary>
        /// On add model
        /// </summary>
        /// <param name="data"></param>
        void OnAdd(TData data);

        /// <summary>
        /// On clear all models
        /// </summary>
        void OnClear();

        /// <summary>
        /// On insert a model in the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        void OnInsert(int index, TData item);

        /// <summary>
        /// On remove a model
        /// </summary>
        /// <param name="data"></param>
        void OnRemove(TData data);

        /// <summary>
        /// On remove a model in the specified index
        /// </summary>
        /// <param name="index"></param>
        void OnRemoveAt(int index);
    }
}