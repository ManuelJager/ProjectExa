using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class GyroscopeTemplatePartial : TemplatePartial<GyroscopeData>
    {
        [SerializeField] private float turningRate; // In tons it supposed to support

        public override BlockBehaviourBase AddSelf(Block block)
        {
            return SetupBehaviour<GyroscopeBehaviour>(block);
        }

        public override GyroscopeData Convert() => new GyroscopeData
        {
            turningRate = turningRate
        };

        public override IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedValue<string>("Turning Rate", turningRate.ToString())
        };
    }
}