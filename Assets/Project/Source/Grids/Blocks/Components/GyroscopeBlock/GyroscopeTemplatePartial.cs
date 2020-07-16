using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class GyroscopeTemplatePartial : TemplatePartial<GyroscopeData>
    {
        [SerializeField] private float turningRate; // In tons it can rotate 360 degrees per second

        public override GyroscopeData Convert() => new GyroscopeData
        {
            turningRate = turningRate
        };

        public ITooltipComponent[] GetComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedWrapper<string>("Turning Rate", turningRate.ToString())
        };
    }
}