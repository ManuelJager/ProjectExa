namespace Exa.Bindings
{
    public interface IObserver<T>
    {
        void OnUpdate(T data);
    }
}