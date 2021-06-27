using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes {
    public class PowerGenerator : Block, IBehaviourMarker<PowerGeneratorData> {
        [SerializeField] private PowerGeneratorBehaviour powerGeneratorBehaviour;

        public BlockBehaviour<PowerGeneratorData> Component {
            get => powerGeneratorBehaviour;
        }

        public override IEnumerable<BlockBehaviour> GetBehaviours() {
            return base.GetBehaviours()
                .Append(powerGeneratorBehaviour);
        }
    }
}