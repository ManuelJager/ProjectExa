using Exa.Schemas;
using System.Linq;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlueprintGridValidator : IValidator<BlueprintGridValidationArgs>
    {
        // TODO: optimize so the list isn't
        public ValidationResult Validate(BlueprintGridValidationArgs validationArgs)
        {
            var result = new ValidationResult(this);

            result.Assert<BlueprintGridHasDisconnectedBlocks>(
                "Blueprint has disconnected blocks",
                () =>
                {
                    // Check if all of the blueprint blocks has neighbours
                    return validationArgs.blueprintBlocks.anchoredBlueprintBlocks.All((Block) =>
                    {
                        return Block.neighbours.Count != 0;
                    });
                });

            result.Assert<BlueprintGridHasMultipleControllers>(
                "Blueprint has multiple controllers",
                () =>
                {
                    var controllerCount = 0;

                    // Check if blueprint doesn't contain multiple neight
                    foreach (var block in validationArgs.blueprintBlocks.anchoredBlueprintBlocks)
                    {
                        if (block.blueprintBlock.RuntimeContext.Category == "Controller")
                        {
                            controllerCount++;
                        }
                    };

                    return controllerCount <= 1;
                });

            return result;
        }
    }
}