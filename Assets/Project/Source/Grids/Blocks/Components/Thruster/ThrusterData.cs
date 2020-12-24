using System;
using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct ThrusterData : IBlockComponentValues
    {
        public float thrust;

        public void AddGridTotals(GridTotals totals) { }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Thrust", $"{thrust}")
        };
    }
}