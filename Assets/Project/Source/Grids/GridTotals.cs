using Exa.Data;
using Exa.Generics;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using Exa.Grids.Blocks;

namespace Exa.Grids
{
    public class GridTotals : ICloneable<GridTotals>
    {
        public IControllerData controllerData;
        
        private BlockContext context;

        public virtual float Mass { get; set; }
        public virtual float Hull { get; set; }
        public virtual Scalar PowerGenerationModifier { get; set; }
        public virtual Scalar PowerConsumptionModifier { get; set; }
        public virtual Scalar PowerStorageModifier { get; set; }
        public virtual Scalar TurningPowerModifier { get; set; }

        public virtual float PowerGeneration => PowerGenerationModifier.GetValue(controllerData.PowerGeneration);
        public virtual float PowerConsumption => PowerConsumptionModifier.GetValue(controllerData.PowerConsumption);
        public virtual float PowerStorage => PowerStorageModifier.GetValue(controllerData.PowerStorage);
        public virtual float TurningPower => TurningPowerModifier.GetValue(controllerData.TurningRate);
        
        public GridTotals()
            : this(BlockContext.DefaultGroup){ }

        public GridTotals(BlockContext context) {
            this.context = context;
        }

        public BlockContext GetInjectedContext() {
            return context;
        }

        public void Reset() {
            Mass = 0f;
            Hull = 0f;
            PowerGenerationModifier = new Scalar();
            PowerConsumptionModifier = new Scalar();
            PowerStorageModifier = new Scalar();
            TurningPowerModifier = new Scalar();
        }

        public GridTotals Clone() {
            return new GridTotals {
                controllerData = controllerData,
                Mass = Mass,
                Hull = Hull,
                PowerGenerationModifier = PowerGenerationModifier,
                PowerConsumptionModifier = PowerConsumptionModifier,
                PowerStorageModifier = PowerStorageModifier,
                TurningPowerModifier = TurningPowerModifier
            };
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
            new TooltipText($"Mass: {Mass}"),
            new TooltipText($"Hull: {Hull}"),
        };
    }
}