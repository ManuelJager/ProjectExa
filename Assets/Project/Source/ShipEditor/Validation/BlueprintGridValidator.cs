using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Utils;
using Exa.Validation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class BlueprintGridValidator : IValidator<BlueprintGridValidationArgs>
    {
        public ValidationResult Validate(BlueprintGridValidationArgs validationArgs)
        {
            var result = new ValidationResult(GetType());

            var controllers = validationArgs.blueprintBlocks
                .Where((block) => block.BlueprintBlock.Template.category.Is(BlockCategory.Controller));

            var controllerCount = controllers.Count();

            if (controllerCount > 1)
            {
                result.Throw<BlueprintGridControllerError>("Cannot have multiple controllers");
            }
            else if (controllerCount == 0)
            {
                result.Throw<BlueprintGridControllerError>("Must have atleast one controller");
            }
            else
            {
                var startingPoint = controllers.FirstOrDefault().GridAnchor;

                result.Assert<BlueprintGridDisconnectedBlocksError>(
                    "Blueprint has disconnected blocks",
                    () => BlueprintBlocksAreConnected(startingPoint, validationArgs.blueprintBlocks));
            }

            return result;
        }

        public bool BlueprintBlocksAreConnected(Vector2Int startingPoint, BlueprintBlocks blocks)
        {
            var visited = new HashSet<Vector2Int>();

            void FloodFill(Vector2Int gridPos)
            {
                if (blocks.ContainsMember(gridPos) && !visited.Contains(gridPos))
                {
                    visited.Add(gridPos);
                    FloodFill(new Vector2Int(gridPos.x - 1, gridPos.y));
                    FloodFill(new Vector2Int(gridPos.x + 1, gridPos.y));
                    FloodFill(new Vector2Int(gridPos.x, gridPos.y - 1));
                    FloodFill(new Vector2Int(gridPos.x, gridPos.y + 1));
                }
            }

            FloodFill(startingPoint);

            return blocks.GetOccupiedTileCount() == visited.Count;
        }
    }
}