using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct StationControllerData : IControllerData
    {
        public float powerGeneration;
        public float powerConsumption;
        public float powerStorage;

        public void AddGridTotals(GridTotals totals)
        {
            totals.controllerData = this;
        }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Generation", $"{powerGeneration} KW"),
            new LabeledValue<object>("Consumption", $"{powerConsumption} KW"),
            new LabeledValue<object>("Storage", $"{powerStorage} KJ"),
        };
    }
}