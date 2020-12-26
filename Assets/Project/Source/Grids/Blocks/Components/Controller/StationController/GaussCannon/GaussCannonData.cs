using System;
using System.Collections.Generic;
using Exa.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct GaussCannonData : ITurretValues
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
            new LabeledValue<object>("Turning rate", $"{turningRate}°/s"),
            new LabeledValue<object>("Firing rate", $"{60 / firingRate} RPM"),
            new LabeledValue<object>("Damage", $"{damage}"),
        };
    }
}