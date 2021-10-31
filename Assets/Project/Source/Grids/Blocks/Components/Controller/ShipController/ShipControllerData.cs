using System;
using System.Collections.Generic;
using Exa.Data;
using Exa.Math;
using Exa.Types.Generics;
using Exa.UI.Tooltips;
using UnityEngine.Serialization;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct ShipControllerData : IControllerData {
        [FormerlySerializedAs("powerGenerationModifier")] public float powerGeneration;
        [FormerlySerializedAs("turningPowerModifier")] public float torque;

        public void AddGridTotals(GridTotals totals) {
            totals.UnscaledPowerGeneration += powerGeneration;
            totals.UnscaledTorque += torque;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.UnscaledPowerGeneration -= powerGeneration;
            totals.UnscaledTorque -= torque;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            return new ITooltipComponent[] {
                new LabeledValue<object>("Power generation", powerGeneration.ToPercentageString()),
                new LabeledValue<object>("Torque", torque.ToPercentageString())
            };
        }
    }
}