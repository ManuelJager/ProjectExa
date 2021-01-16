using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Utils;
using Exa.Validation;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class BlueprintGridValidator : Validator<BlueprintGridValidationArgs>
    {
        protected override void AddErrors(ValidationResult errors, BlueprintGridValidationArgs args) {
            var blocks = args.blueprintBlocks;

            var controllerCount = blocks.Count(block => block.GetIsController());
            if (controllerCount > 1) {
                errors.Throw<ControllerError>("Cannot have multiple controllers");
            }
            else if (controllerCount == 0) {
                errors.Throw<ControllerError>("Must have at least one controller");
            }

            if (blocks.Count() > 1 && BlocksAreDisconnected(blocks)) {
                errors.Throw<DisconnectedBlocksError>("Blueprint has disconnected blocks");
            }
        }

        private bool BlocksAreDisconnected(BlueprintBlocks blocks) {
            return blocks.Any(block => blocks.GetNeighbourCount(block) == 0);
        }
    }
}