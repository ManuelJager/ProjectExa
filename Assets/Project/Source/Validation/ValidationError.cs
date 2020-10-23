namespace Exa.Validation
{
    /// <summary>
    /// Supports a validation error
    /// </summary>
    public abstract class ValidationError
    {
        protected ValidationError() { }

        public ErrorType ErrorType { get; set; } = ErrorType.Error;
        public string Message { get; set; }
        public virtual string Id => GetType().Name;
    }
}