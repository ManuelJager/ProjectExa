using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Utils;
using Exa.Validation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class BlueprintGridValidator : Validator<BlueprintGridValidationArgs>
    {
        protected override void AddErrors(ValidationResult errors, BlueprintGridValidationArgs args) {
            var blocks = args.blueprintBlocks;

            var controllers = blocks
                .Where(block => block.BlueprintBlock.Template.category.Is(BlockCategory.ShipController));

            var controllerCount = controllers.Count();

            if (controllerCount > 1) {
                errors.Throw<ControllerError>("Cannot have multiple controllers");
            }
            else if (controllerCount == 0) {
                errors.Throw<ControllerError>("Must have at least one controller");
            }

            if (blocks.Any(block => blocks.GetNeighbourCount(block) == 0)) {
                errors.Throw<DisconnectedBlocksError>("Blueprint has disconnected blocks");
            }
        }

        public bool BlueprintBlocksAreConnected(Vector2Int startingPoint, BlueprintBlocks blocks) {
            var visited = new HashSet<Vector2Int>();

            void FloodFill(Vector2Int gridPos) {
                if (blocks.ContainsMember(gridPos) && !visited.Contains(gridPos)) {
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