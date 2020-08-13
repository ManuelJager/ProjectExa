using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IThrusterTemplatePartial
    {
        ThrusterTemplatePartial ThrusterTemplatePartial { get; }
    }

    [Serializable]
    public class ThrusterTemplatePartial : TemplatePartial<ThrusterData>
    {
        [SerializeField] private int newtonThrust; // In newton

        public int NewtonThrust => newtonThrust;

        public override ThrusterData Convert() => new ThrusterData
        {
            newtonThrust = newtonThrust
        };

        public override void SetValues(Block block)
        {
            (block as IThruster).ThrusterBehaviour.SetData(Convert());
        }

        public override ITooltipComponent[] GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedValue<string>("Thrust", $"{newtonThrust}N")
        };
    }
}