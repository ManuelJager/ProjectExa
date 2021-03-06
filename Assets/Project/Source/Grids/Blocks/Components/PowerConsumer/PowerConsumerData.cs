﻿using System;
using Exa.Data;
using Exa.Generics;
using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct PowerConsumerData : IBlockComponentValues
    {
        public Scalar powerConsumption;

        public void AddGridTotals(GridTotals totals) {
            totals.PowerConsumptionModifier += powerConsumption;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.PowerConsumptionModifier -= powerConsumption;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Power consumption", $"{powerConsumption} KW")
        };
    }
}