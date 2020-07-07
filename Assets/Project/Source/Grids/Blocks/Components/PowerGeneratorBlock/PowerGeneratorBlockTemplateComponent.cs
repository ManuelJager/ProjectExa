using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerGeneratorBlockTemplateComponent : ITemplateComponent<PowerGeneratorBlockData>
    {
        [SerializeField] private float peakGeneration; // In MW

        public float PeakGeneration => peakGeneration;

        public PowerGeneratorBlockData Convert()
        {
            return new PowerGeneratorBlockData
            {
            };
        }

        public ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new TooltipSpacer(),
                new NamedWrapper<string>("Power generation", $"{peakGeneration} KW")
            };
        }
    }
}