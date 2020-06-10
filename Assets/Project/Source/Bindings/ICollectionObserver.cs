namespace Exa.Bindings
{
    public interface ICollectionObserver<TData>
    {
        IObservableCollection<TData> Source { get; set; }

        void OnSet(int index, TData item);

        void OnAdd(TData data);

        void OnClear();

        void OnInsert(int index, TData item);

        void OnRemove(TData data);

        void OnRemoveAt(int index);
    }
}