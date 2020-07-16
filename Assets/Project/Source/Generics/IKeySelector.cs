namespace Exa.Generics
{
    public interface IKeySelector<T>
    {
        T Key { get; }
    }
}