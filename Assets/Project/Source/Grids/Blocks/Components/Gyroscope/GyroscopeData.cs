using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;
using UnityEngine.Serialization;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct GyroscopeData : IBlockComponentValues {
        [FormerlySerializedAs("turningPower")] public float torque;

        public void AddGridTotals(GridTotals totals) {
            totals.UnscaledTorque += torque;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.UnscaledTorque -= torque;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            return new ITooltipComponent[] {
                new LabeledValue<object>("Turning Rate", torque.ToString())
            };
        }
    }
}