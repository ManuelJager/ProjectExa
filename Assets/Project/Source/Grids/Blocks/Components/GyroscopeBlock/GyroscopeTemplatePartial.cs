using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
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

        public override void SetValues(Block block)
        {
            (block as IGyroscope).GyroscopeBehaviour.SetData(Convert());
        }

        public override ITooltipComponent[] GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedValue<string>("Turning Rate", turningRate.ToString())
        };
    }
}