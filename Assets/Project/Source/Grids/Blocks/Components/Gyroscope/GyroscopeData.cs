﻿using Exa.Data;
using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct GyroscopeData : IBlockComponentValues
    {
        public Scalar turningRate;

        public void AddGridTotals(GridTotals totals) {
            totals.TurningPowerModifier += turningRate;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.TurningPowerModifier -= turningRate;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Turning Rate", turningRate.ToString())
        };
    }
}