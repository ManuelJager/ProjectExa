using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerGeneratorTemplatePartial : TemplatePartial<PowerGeneratorData>
    {
        [SerializeField] private float peakGeneration; // In MW

        public override BlockBehaviourBase AddSelf(Block block)
        {
            return SetupBehaviour<PowerGeneratorBehaviour>(block);
        }

        public override PowerGeneratorData Convert() => new PowerGeneratorData
        {
            peakGeneration = peakGeneration
        };

        public override IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new LabeledValue<string>("Power generation", $"{peakGeneration} KW")
        };
    }
}