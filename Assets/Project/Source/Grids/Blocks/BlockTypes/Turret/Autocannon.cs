using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes {
    public class Autocannon : Block, IBehaviourMarker<AutocannonData> {
        public AutocannonBehaviour turretBehaviour;

        BlockBehaviour<AutocannonData> IBehaviourMarker<AutocannonData>.Component {
            get => turretBehaviour;
        }

        public override IEnumerable<BlockBehaviour> GetBehaviours() {
            return base.GetBehaviours().Append(turretBehaviour);
        }

        public void ForceActive() {
            foreach (var behaviour in GetBehaviours()) {
                behaviour.ForceActive();
            }
        }
    }
}