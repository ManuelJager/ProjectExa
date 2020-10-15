namespace Exa.Validation
{
    public class GenericValidationError : ValidationError
    {
        public override string Id { get; }

        public GenericValidationError(string id, string message)
        {
            this.Id = id;
            this.Message = message;
        }

        public GenericValidationError(string id, string message, ErrorType errorType)
            : this(id, message)
        {
            this.ErrorType = errorType;
        }
    }
}