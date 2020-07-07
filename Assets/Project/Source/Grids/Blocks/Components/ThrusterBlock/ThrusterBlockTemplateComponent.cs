using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ThrusterBlockTemplateComponent : ITemplateComponent<ThrusterBlockData>
    {
        [SerializeField] private int newtonThrust; // In newton

        public int NewtonThrust => newtonThrust;

        public ThrusterBlockData Convert()
        {
            return new ThrusterBlockData
            {
                newtonThrust = newtonThrust
            };
        }

        public ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new TooltipSpacer(),
                new NamedWrapper<string>("Thrust", $"{newtonThrust}N")
            };
        }
    }
}