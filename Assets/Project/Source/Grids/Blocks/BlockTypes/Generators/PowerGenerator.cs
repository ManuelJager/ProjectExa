using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PowerGenerator : Block, IBehaviourMarker<PowerGeneratorData>
    {
        [SerializeField] private PowerGeneratorBehaviour powerGeneratorBehaviour;

        public BlockBehaviour<PowerGeneratorData> Component => powerGeneratorBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours() {
            return base.GetBehaviours()
                .Append(powerGeneratorBehaviour);
        }
    }
}