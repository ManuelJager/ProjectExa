using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct GaussCannonData : IChargeableTurretValues
    {
        public float turningRate;
        public float firingRate;
        public float damage;
        public float chargeTime;

        public float TurningRate => turningRate;
        public float FiringRate => firingRate;
        public float Damage => damage;
        public float ChargeTime => chargeTime;

        public void AddGridTotals(GridTotals totals) { }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Turning rate", $"{turningRate}°/s"),
            new LabeledValue<object>("Firing rate", $"{60 / firingRate} RPM"),
            new LabeledValue<object>("Damage", $"{damage}"),
        };
    }
}