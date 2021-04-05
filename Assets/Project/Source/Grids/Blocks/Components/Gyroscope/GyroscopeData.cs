﻿using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using Exa.Types.Generics;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct GyroscopeData : IBlockComponentValues
    {
        public float turningPower;
         
        public void AddGridTotals(GridTotals totals) {
            totals.UnscaledTurningPower += turningPower;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.UnscaledTurningPower += turningPower;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[] {
            new LabeledValue<object>("Turning Rate", turningPower.ToString())
        };
    }
}