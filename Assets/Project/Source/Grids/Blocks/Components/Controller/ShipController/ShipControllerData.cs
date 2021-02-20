using System;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using Exa.Data;
using Exa.Types.Generics;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct ShipControllerData : IControllerData
    {
        public float powerGeneration;
        public float powerConsumption;
        public float powerStorage;
        public float turningRate;
        public Scalar thrustModifier;
        
        public float PowerGeneration => powerGeneration;
        public float PowerConsumption => powerConsumption;
        public float PowerStorage => powerStorage;
        public float TurningRate => turningRate;
        
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