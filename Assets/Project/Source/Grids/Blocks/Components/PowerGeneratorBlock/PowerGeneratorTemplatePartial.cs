using Exa.Generics;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerGeneratorTemplatePartial : TemplatePartial<PowerGeneratorData>, IBlueprintTotalsModifier
    {
        [SerializeField] private float peakGeneration; // In MW

        public virtual void AddBlueprintTotals(Blueprint blueprint)
        {
            blueprint.PeakPowerGeneration += peakGeneration;
        }

        public virtual void RemoveBlueprintTotals(Blueprint blueprint)
        {
            blueprint.PeakPowerGeneration -= peakGeneration;
        }

        public override PowerGeneratorData Convert() => new PowerGeneratorData
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