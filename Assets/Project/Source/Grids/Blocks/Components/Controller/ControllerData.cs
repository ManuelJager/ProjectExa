using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using Exa.Data;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public struct ControllerData : IBlockComponentValues
    {
        public float powerGeneration;
        public float powerConsumption;
        public float powerStorage;
        public float turningRate;
        public Scalar thrustModifier;

        public void AddGridTotals(GridTotals totals)
        {
            totals.controllerData = this;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new LabeledValue<string>("Generation", $"{powerGeneration} KW"),
            new LabeledValue<string>("Consumption", $"{powerConsumption} KW"),
            new LabeledValue<string>("Storage", $"{powerStorage} KJ"),
            new LabeledValue<string>("Turning rate", $"{turningRate}"),
            new LabeledValue<string>("ThrustModifier", $"{thrustModifier.ToPercentageString()}")
        };
    }
}