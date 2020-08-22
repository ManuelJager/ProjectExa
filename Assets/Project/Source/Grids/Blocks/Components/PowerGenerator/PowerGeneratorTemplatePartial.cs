using Exa.Data;
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
        [SerializeField] private Percentage peakGeneration;

        public override PowerGeneratorData Convert() => new PowerGeneratorData
        {
            powerGeneration = peakGeneration
        };

        public override IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new LabeledValue<string>("Power generation", $"{peakGeneration} KW")
        };
    }
}