using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    public struct TurretData : IBlockComponentValues
    {
        public float turningRate; // in degrees rotation per second
        public float firingRate; // in rounds per second

        // TODO: Expand the damage model
        public float damage;

        public void AddGridTotals(GridTotals totals)
        {
        }

        public void RemoveGridTotals(GridTotals totals)
        {
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new LabeledValue<string>("Turning rate", $"{turningRate}°/s"),
            new LabeledValue<string>("Firing rate", $"{firingRate * 60} RPM"),
            new LabeledValue<string>("Damage", $"{damage}")
        };
    }
}