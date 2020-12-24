using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct PhysicalData : IBlockComponentValues
    {
        public float hull;
        public float armor;
        public float mass;

        public void AddGridTotals(GridTotals totals) {
            totals.Mass += mass;
            totals.Hull += hull;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.Mass -= mass;
            totals.Hull -= hull;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Hull", $"{hull}"),
            new LabeledValue<object>("Armor", $"{armor}"),
            new LabeledValue<object>("Mass", $"{mass * 1000:0} KG")
        };
    }
}