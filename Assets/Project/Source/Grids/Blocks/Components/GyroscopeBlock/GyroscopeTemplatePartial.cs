using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IGyroscopeTemplatePartial
    {
        GyroscopeTemplatePartial GyroscopeTemplatePartial { get; }
    }

    [Serializable]
    public class GyroscopeTemplatePartial : TemplatePartial<GyroscopeData>
    {
        [SerializeField] private float turningRate; // In tons it supposed to support

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