using Exa.Grids.Blocks;
using Exa.ShipEditor;
using Exa.Validation;

namespace Exa.Gameplay.Missions
{
    public class BlueprintCostValidator : Validator<BlueprintCostValidatorArgs>
    {
        private BlockCosts maxAllowedCosts;

        public BlueprintCostValidator(BlockCosts budget, BlockCosts currentCosts) {
            maxAllowedCosts = budget + currentCosts;
        }
        
        protected override void AddErrors(ValidationResult errors, BlueprintCostValidatorArgs args) {
            if (args.currentCosts.creditCost > maxAllowedCosts.creditCost ||
                args.currentCosts.metalsCost > maxAllowedCosts.metalsCost) {
                errors.Throw<BlueprintCostError>("Costs exceed budget");
            }
        }
    }

    public class BlueprintCostValidatorArgs
    {
        public BlockCosts currentCosts;
    }
    
    public class BlueprintCostError : ValidationError { }
}