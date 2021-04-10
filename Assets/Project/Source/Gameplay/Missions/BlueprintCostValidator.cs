using Exa.Grids.Blocks;
using Exa.Validation;

namespace Exa.Gameplay.Missions
{
    public class BlueprintCostValidator : PlugableValidator<BlueprintCostValidatorArgs>
    {
        private BlockCosts maxAllowedCosts;

        public BlueprintCostValidator(BlockCosts budget, BlockCosts currentCosts) {
            maxAllowedCosts = budget + currentCosts;
            
            BlueprintChangedHandler();
            Systems.Editor.BlueprintChangedEvent += BlueprintChangedHandler;
        }
        
        protected override void AddErrors(ValidationResult errors, BlueprintCostValidatorArgs args) {
            if (args.currentCosts.creditCost > maxAllowedCosts.creditCost ||
                args.currentCosts.metalsCost > maxAllowedCosts.metalsCost) {
                errors.Throw<BlueprintCostError>("Costs exceed budget");
            }
        }
        
        void BlueprintChangedHandler() { 
            var args = new BlueprintCostValidatorArgs {
                currentCosts = Systems.Editor.ActiveBlueprintTotals.Metadata.blockCosts,
            }; 
    
            Result = Systems.Editor.Validate(this, args);
        }
        
        public override void CleanUp() {
            Systems.Editor.BlueprintChangedEvent -= BlueprintChangedHandler;
        }
    }

    public class BlueprintCostValidatorArgs
    {
        public BlockCosts currentCosts;
    }
    
    public class BlueprintCostError : ValidationError { }
}