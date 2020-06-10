using Exa.Schemas;
using System.Linq;

namespace Exa.Grids.Blueprints
{
    public class BlueprintValidationArgs
    {
        public ObservableBlueprintCollection collectionContext;
        public ObservableBlueprint blueprintContainer;
        public string requestedName;
    }

    public class CollectionContainsBlueprintNameError : ValidationError
    {
        public CollectionContainsBlueprintNameError(string message)
            : base("CollectionContainsBlueprintNameError", message)
        {
        }
    }

    public class BlueprintHasDefaultNameError : ValidationError
    {
        public BlueprintHasDefaultNameError(string message)
            : base("BlueprintHasDefaultNameError", message)
        {
        }
    }

    public class BlueprintHasEmptyNameError : ValidationError
    {
        public BlueprintHasEmptyNameError(string message)
            : base("BlueprintHasEmptyNameError", message)
        {
        }
    }

    public class BlueprintNameValidator : IValidator<BlueprintValidationArgs>
    {
        public ValidationResult Validate(BlueprintValidationArgs validationArgs)
        {
            var errors = new ValidationResult(this);

            // Check if there is any blueprint in the collection that contains the same name as the requested name
            errors.Assert<CollectionContainsBlueprintNameError>(
                $"Blueprint name is already used", () =>
                {
                    return !validationArgs.collectionContext.Any((blueprintContainer) =>
                    blueprintContainer.Data.name == validationArgs.requestedName &&
                    blueprintContainer != validationArgs.blueprintContainer);
                });

            // Check if the name isn't empty
            errors.Assert<BlueprintHasEmptyNameError>(
                $"Blueprint name is empty", () =>
                {
                    return validationArgs.requestedName != "";
                });

            // Check if the requested name is the default name
            errors.Assert<BlueprintHasDefaultNameError>(
                "Blueprint name cannot be default", () =>
                {
                    return validationArgs.requestedName != Blueprint.DEFAULT_BLUEPRINT_NAME;
                });

            return errors;
        }
    }
}