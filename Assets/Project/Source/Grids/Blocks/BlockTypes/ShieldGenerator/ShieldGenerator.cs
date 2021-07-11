using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    public class ShieldGenerator : Block, IBehaviourMarker<ShieldGeneratorData> {
        [SerializeField] private ShieldGeneratorBehaviour shieldGeneratorBehaviour;

        public BlockBehaviour<ShieldGeneratorData> Component {
            get => shieldGeneratorBehaviour;
        }

        public override IEnumerable<BlockBehaviour> GetBehaviours() {
            return base.GetBehaviours().Append(shieldGeneratorBehaviour);
        }
    }
}