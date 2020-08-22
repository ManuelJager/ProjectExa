using Exa.Data;
using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class GyroscopeTemplatePartial : TemplatePartial<GyroscopeData>
    {
        [SerializeField] private Percentage turningRate;

        public override GyroscopeData Convert() => new GyroscopeData
        {
            turningRate = turningRate
        };

        public override IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new LabeledValue<string>("Turning Rate", turningRate.ToString())
        };
    }
}