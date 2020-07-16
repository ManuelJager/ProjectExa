using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ThrusterTemplatePartial : TemplatePartial<ThrusterData>
    {
        [SerializeField] private int newtonThrust; // In newton

        public int NewtonThrust => newtonThrust;

        public override ThrusterData Convert() => new ThrusterData
        {
            newtonThrust = newtonThrust
        };

        public ITooltipComponent[] GetComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedWrapper<string>("Thrust", $"{newtonThrust}N")
        };
    }
}