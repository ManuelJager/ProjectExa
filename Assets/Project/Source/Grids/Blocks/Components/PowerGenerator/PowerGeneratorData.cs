using System;
using Exa.Data;
using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
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
            new LabeledValue<object>("Power generation", $"{powerGeneration}")
        };
    }
}