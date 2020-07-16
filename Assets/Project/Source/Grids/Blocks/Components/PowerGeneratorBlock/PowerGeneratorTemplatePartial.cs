using Exa.Generics;
using Exa.Grids.Blueprints;
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

        public virtual void AddContext(Blueprint blueprint)
        {
            blueprint.PeakPowerGeneration += peakGeneration;
        }

        public virtual void RemoveContext(Blueprint blueprint)
        {
            blueprint.PeakPowerGeneration -= peakGeneration;
        }

        public PowerGeneratorData Convert() => new PowerGeneratorData
        {
            peakGeneration = peakGeneration
        };

        public ITooltipComponent[] GetComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedWrapper<string>("Power generation", $"{peakGeneration} KW")
        };
    }
}