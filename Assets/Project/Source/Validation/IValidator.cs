namespace Exa.Validation
{
    /// <summary>
    /// Supports an object that validates a model
    /// </summary>
    /// <typeparam name="TArgs">Validation model</typeparam>
    public interface IValidator<TArgs>
    {
        /// <summary>
        /// Validate the model and return a result
        /// </summary>
        /// <param name="validationArgs">Validation model</param>
        /// <returns>Collection of errors</returns>
        ValidationResult Validate(TArgs validationArgs);
    }
}