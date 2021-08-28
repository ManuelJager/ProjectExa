using System;
using System.Collections.Generic;
using Exa.Data;
using Exa.Math;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct StationControllerData : IControllerData {
        public float powerGenerationModifier;
        public float turningPowerModifier;

        public void AddGridTotals(GridTotals totals) {
            totals.UnscaledPowerGeneration += powerGenerationModifier;
            totals.UnscaledTurningPower += turningPowerModifier;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.UnscaledPowerGeneration -= powerGenerationModifier;
            totals.UnscaledTurningPower -= turningPowerModifier;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            return new ITooltipComponent[] {
                new LabeledValue<object>("Generation", powerGenerationModifier.ToPercentageString()),
                new LabeledValue<object>("Consumption", turningPowerModifier.ToPercentageString())
            };
        }
    }
}