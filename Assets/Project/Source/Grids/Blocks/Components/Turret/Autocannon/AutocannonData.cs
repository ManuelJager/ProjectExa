using System;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using Exa.Types.Generics;

namespace Exa.Grids.Blocks.Components
{
    public enum CycleMode
    {
        Cycling,
        Volley
    }

    [Serializable]
    public struct AutocannonData : ITurretValues
    {
        public float turningRate;
        public float firingRate;
        public float damage;
        public CycleMode cycleMode;

        public float TurningRate => turningRate;
        public float FiringRate => firingRate;
        public float Damage => damage;

        public void AddGridTotals(GridTotals totals) { }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Turning rate", $"{turningRate}°/s"),
            new LabeledValue<object>("Firing rate", $"{60 / firingRate} RPM"),
            new LabeledValue<object>("Damage", $"{damage}"),
            new LabeledValue<object>("Cycle mode", $"{cycleMode}")
        };
    }
}