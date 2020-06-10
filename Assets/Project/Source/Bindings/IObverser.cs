namespace Exa.Bindings
{
    public interface IObserver<TData>
    {
        void OnUpdate(TData data);
    }
}