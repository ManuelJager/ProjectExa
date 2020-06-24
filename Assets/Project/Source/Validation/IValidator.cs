namespace Exa.Validation
{
    public interface IValidator<TArgs>
    {
        ValidationResult Validate(TArgs validationArgs);
    }
}