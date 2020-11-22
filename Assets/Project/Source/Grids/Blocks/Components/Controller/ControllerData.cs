using System;
using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using Exa.Data;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct ControllerData : IBlockComponentValues
    {
        public float powerGeneration;
        public float powerConsumption;
        public float powerStorage;
        public float turningRate;
        public Scalar thrustModifier;

        public void AddGridTotals(GridTotals totals) {
            totals.controllerData = this;
        }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Generation", $"{powerGeneration} KW"),
            new LabeledValue<object>("Consumption", $"{powerConsumption} KW"),
            new LabeledValue<object>("Storage", $"{powerStorage} KJ"),
            new LabeledValue<object>("Turning rate", $"{turningRate}"),
            new LabeledValue<object>("Thrust modifier", $"{thrustModifier.ToPercentageString()}")
        };
    }
}