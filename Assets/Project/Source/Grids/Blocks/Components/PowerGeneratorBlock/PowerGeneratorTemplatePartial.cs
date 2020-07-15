using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerGeneratorTemplatePartial : ITemplatePartial<PowerGeneratorData>
    {
        [SerializeField] private float peakGeneration; // In MW

        public float PeakGeneration => peakGeneration;

        public PowerGeneratorData Convert() => new PowerGeneratorData
        {
        };

        public ITooltipComponent[] GetComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedWrapper<string>("Power generation", $"{peakGeneration} KW")
        };
    }
}