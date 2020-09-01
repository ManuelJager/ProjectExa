using Exa.Data;
using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    public struct ThrusterData : IBlockComponentValues
    {
        public Percentage thrust;

        public void AddGridTotals(GridTotals totals)
        {
        }

        public void RemoveGridTotals(GridTotals totals)
        {
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new LabeledValue<string>("Thrust", $"{thrust}")
        };
    }
}