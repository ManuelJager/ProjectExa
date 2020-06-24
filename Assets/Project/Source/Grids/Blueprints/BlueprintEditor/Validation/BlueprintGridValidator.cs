using Exa.Validation;
using System.Linq;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlueprintGridValidator : IValidator<BlueprintGridValidationArgs>
    {
        public ValidationResult Validate(BlueprintGridValidationArgs validationArgs)
        {
            var result = new ValidationResult();

            var thrownBlueprintGridHasDisconnectedBlocks = false;
            var thrownBlueprintGridHasMultipleControllers = false;
            var controllerCount = 0;

            foreach (var block in validationArgs.blueprintBlocks.anchoredBlueprintBlocks)
            {
                // Check for disconnected blocks
                result.Assert<BlueprintGridHasDisconnectedBlocks>(
                    "Blueprint has disconnected blocks", 
                    () => block.neighbours.Count > 0,
                    ref thrownBlueprintGridHasDisconnectedBlocks);

                // Check for multiple controllers
                result.Assert<BlueprintGridHasMultipleControllers>(
                    "Blueprint has multiple controllers",
                    () =>
                    {
                        if (block.blueprintBlock.RuntimeContext.Category == "Controller")
                        {
                            controllerCount++;
                        }

                        return controllerCount <= 1;
                    },
                    ref thrownBlueprintGridHasMultipleControllers);
            }

            return result;
        }
    }
}