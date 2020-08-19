using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ThrusterTemplatePartial : TemplatePartial<ThrusterData>
    {
        [SerializeField] private int newtonThrust; // In newton

        public int NewtonThrust => newtonThrust;

        public override BlockBehaviourBase AddSelf(Block block)
        {
            return SetupBehaviour<ThrusterBehaviour>(block);
        }

        public override ThrusterData Convert() => new ThrusterData
        {
            thrust = newtonThrust
        };

        public override IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new LabeledValue<string>("Thrust", $"{newtonThrust}N")
        };
    }
}