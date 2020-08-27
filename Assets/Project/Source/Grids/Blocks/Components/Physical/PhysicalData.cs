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
        public short mass;

        public void AddGridTotals(GridTotals totals)
        {
            totals.Mass += mass;
            totals.Hull += hull;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.Mass -= mass;
            totals.Hull -= hull;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new LabeledValue<string>("Hull", hull.ToString()),
            new LabeledValue<string>("Armor", armor.ToString()),
            new LabeledValue<string>("Mass", mass.ToString())
        };
    }
}