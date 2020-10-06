namespace Exa.Validation
{
    public abstract class Validator<T> : IValidator<T>
    {
        public ValidationResult Validate(T args)
        {
            var errors = new ValidationResult(this);

            return errors;
        }

        protected abstract void AddErrors(ValidationResult errors, T args);
    }
}