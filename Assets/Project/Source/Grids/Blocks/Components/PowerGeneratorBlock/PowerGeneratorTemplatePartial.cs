using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IPowerGeneratorTemplatePartial
    {
        PowerGeneratorTemplatePartial PowerGeneratorTemplatePartial { get; }
    }

    [Serializable]
    public class PowerGeneratorTemplatePartial : TemplatePartial<PowerGeneratorData>
    {
        [SerializeField] private float peakGeneration; // In MW

        public override PowerGeneratorData Convert() => new PowerGeneratorData
        {
            peakGeneration = peakGeneration
        };

        public override void SetValues(Block block)
        {
            (block as IPowerGenerator).PowerGeneratorBehaviour.SetData(Convert());
        }

        public override ITooltipComponent[] GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedValue<string>("Power generation", $"{peakGeneration} KW")
        };
    }
}