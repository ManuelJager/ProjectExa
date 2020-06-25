namespace Exa.Validation
{
    /// <summary>
    /// Supports a validation error
    /// </summary>
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