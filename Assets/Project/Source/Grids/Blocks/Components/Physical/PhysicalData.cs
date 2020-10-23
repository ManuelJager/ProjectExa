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
            new LabeledValue<string>("Hull", $"{hull}"),
            new LabeledValue<string>("Armor", $"{armor}"),
            new LabeledValue<string>("Mass", $"{mass * 1000:0} KG")
        };
    }
}