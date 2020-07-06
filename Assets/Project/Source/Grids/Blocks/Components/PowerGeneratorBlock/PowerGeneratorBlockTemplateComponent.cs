using Exa.Generics;
using Exa.UI.Controls;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerGeneratorBlockTemplateComponent : ITemplateComponent<PowerGeneratorBlockData>, ITooltipPresenter
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
                new NamedValue<string> { Name = "Power generation", Value = $"{peakGeneration} KW"}
            };
        }
    }
}