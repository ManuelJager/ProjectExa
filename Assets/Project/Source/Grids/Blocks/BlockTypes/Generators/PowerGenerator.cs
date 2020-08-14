using Exa.Ships;
using Exa.Grids.Blocks.Components;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PowerGenerator : Block, IPowerGenerator
    {
        [SerializeField] private PowerGeneratorBehaviour powerGeneratorBehaviour;

        BlockBehaviour<PowerGeneratorData> IBehaviourMarker<PowerGeneratorData>.Component
        { 
            get => powerGeneratorBehaviour; 
            set => powerGeneratorBehaviour = value as PowerGeneratorBehaviour; 
        }

        protected override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(powerGeneratorBehaviour);
        }
    }
}