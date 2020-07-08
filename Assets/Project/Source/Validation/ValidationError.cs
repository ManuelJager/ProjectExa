namespace Exa.Validation
{
    /// <summary>
    /// Supports a validation error
    /// </summary>
    public abstract class ValidationError
    {
        public ValidationError()
        {
        }

        public string Message { get; set; }
        public virtual string Id => GetType().Name;
    }
}