namespace Exa.Schemas
{
    public interface IValidator<TArgs>
    {
        ValidationResult Validate(TArgs validationArgs);
    }
}