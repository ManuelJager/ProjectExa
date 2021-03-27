using System;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using Exa.Data;
using Exa.Types.Generics;
using Unity.Entities;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct ShipControllerData : IControllerData
    {
        public Scalar powerGenerationModifier;
        public Scalar turningPowerModifier;
        public Scalar thrustModifier;

        public void AddGridTotals(GridTotals totals) {
            totals.PowerGenerationModifier += powerGenerationModifier;
            totals.TurningPowerModifier += turningPowerModifier;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.PowerGenerationModifier -= powerGenerationModifier;
            totals.TurningPowerModifier -= turningPowerModifier;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Generation", powerGenerationModifier.ToPercentageString()),
            new LabeledValue<object>("Consumption", turningPowerModifier.ToPercentageString())
        };
    }
}