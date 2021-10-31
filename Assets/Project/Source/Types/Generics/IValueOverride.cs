namespace Exa.Types.Generics {
    public interface IValueOverride<out T> {
        public T Value { get; }
    }
}