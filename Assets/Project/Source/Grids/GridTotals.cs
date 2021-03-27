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
        private BlockContext context;

        public float Mass { get; set; }
        public float Hull { get; set; }
        public float UnscaledPowerGeneration { get; set; }
        public float UnscaledTurningPower { get; set; }
        public Scalar PowerGenerationModifier { get; set; }
        public Scalar TurningPowerModifier { get; set; }
        public BlockMetadata Metadata { get; set; }
        
        public float PowerGeneration => PowerGenerationModifier.GetValue(UnscaledPowerGeneration);
        public float TurningPower => TurningPowerModifier.GetValue(UnscaledTurningPower);
        
        public GridTotals(BlockContext context) {
            this.context = context;
        }

        public BlockContext GetInjectedContext() {
            return context;
        }

        public void Reset() {
            Mass = 1f;
            Hull = 1f;
            PowerGenerationModifier = new Scalar();
            TurningPowerModifier = new Scalar();
        }

        public GridTotals Clone() => new GridTotals(context) {
            Mass = Mass,
            Hull = Hull,
            PowerGenerationModifier = PowerGenerationModifier,
            TurningPowerModifier = TurningPowerModifier
        };

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
            new TooltipText($"Mass: {Mass}"),
            new TooltipText($"Hull: {Hull}"),
        };
    }
}