using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerConsumerTemplatePartial : TemplatePartial<PowerConsumerData>
    {
        [SerializeField] private float powerConsumption; // In MW

        public override BlockBehaviourBase AddSelf(Block block)
        {
            return SetupBehaviour<PowerConsumerBehaviour>(block);
        }

        public override PowerConsumerData Convert() => new PowerConsumerData
        {
            powerConsumption = powerConsumption
        };

        public override IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new LabeledValue<string>("Power consumption", $"{powerConsumption} KW")
        };
    }
}