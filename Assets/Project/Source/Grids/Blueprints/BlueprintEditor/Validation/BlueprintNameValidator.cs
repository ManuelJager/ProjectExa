using Exa.Validation;
using System.Linq;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlueprintNameValidator : IValidator<BlueprintNameValidationArgs>
    {
        public ValidationResult Validate(BlueprintNameValidationArgs validationArgs)
        {
            var errors = new ValidationResult();

            // Check if there is any blueprint in the collection that contains the same name as the requested name
            errors.Assert<BlueprintNameDuplicateError>(
                $"Blueprint name is already used", () =>
                {
                    return !validationArgs.collectionContext.Any((blueprintContainer) =>
                    blueprintContainer.Data.name == validationArgs.requestedName &&
                    blueprintContainer != validationArgs.blueprintContainer);
                });

            // Check if the name isn't empty
            errors.Assert<BlueprintNameEmptyError>(
                $"Blueprint name is empty", 
                () => validationArgs.requestedName != "");

            // Check if the requested name is the default name
            errors.Assert<BlueprintNameDefaultError>(
                "Blueprint name cannot be default", 
                () => validationArgs.requestedName != Blueprint.DEFAULT_BLUEPRINT_NAME);

            return errors;
        }
    }
}