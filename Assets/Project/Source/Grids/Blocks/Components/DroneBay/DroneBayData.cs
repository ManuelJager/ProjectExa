using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct DroneBayData : IBlockComponentValues {
        public float buildTime;
        public int maxPopulation;
        
        public void AddGridTotals(GridTotals totals) {
        }

        public void RemoveGridTotals(GridTotals totals) {
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            yield return new LabeledValue<object>("Build time", $"{buildTime}");
            yield return new LabeledValue<object>("Max population", $"{maxPopulation}");
        }
    }
}