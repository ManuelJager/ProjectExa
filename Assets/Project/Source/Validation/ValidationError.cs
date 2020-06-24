namespace Exa.Validation
{
    public abstract class ValidationError
    {
        public ValidationError(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
        public virtual string Id => GetType().Name;
    }
}