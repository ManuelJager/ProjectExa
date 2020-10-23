using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    public struct TurretData : ITurretValues
    {
        public float turningRate;
        public float firingRate;
        public float damage;

        public float TurningRate => turningRate;
        public float FiringRate => firingRate;
        public float Damage => damage;

        public void AddGridTotals(GridTotals totals) { }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<string>("Turning rate", $"{turningRate}°/s"),
            new LabeledValue<string>("Firing rate", $"{firingRate * 60} RPM"),
            new LabeledValue<string>("Damage", $"{damage}")
        };
    }
}