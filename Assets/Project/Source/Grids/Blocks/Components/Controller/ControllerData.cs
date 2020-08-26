using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public struct ControllerData : IBlockComponentData
    {
        public float powerGeneration;
        public float powerConsumption;
        public float powerStorage;
        public float turningRate;
        public float directionalForce;

        public void AddGridTotals(GridTotals totals)
        {
            totals.controllerData = this;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new LabeledValue<string>("Power generation", $"{powerGeneration} KW"),
            new LabeledValue<string>("Power consumption", $"{powerConsumption} KW"),
            new LabeledValue<string>("Power storage", $"{powerStorage} KJ"),
            new LabeledValue<string>("Turning rate", $"{turningRate}"),
            new LabeledValue<string>("Directional force", $"{directionalForce} N")
        };
    }
}