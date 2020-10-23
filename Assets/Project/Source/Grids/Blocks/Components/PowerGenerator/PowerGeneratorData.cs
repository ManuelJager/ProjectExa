using Exa.Data;
using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    public struct PowerGeneratorData : IBlockComponentValues
    {
        public Scalar powerGeneration;

        public void AddGridTotals(GridTotals totals) {
            totals.PowerGenerationModifier += powerGeneration;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.PowerGenerationModifier -= powerGeneration;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<string>("Power generation", $"{powerGeneration}")
        };
    }
}