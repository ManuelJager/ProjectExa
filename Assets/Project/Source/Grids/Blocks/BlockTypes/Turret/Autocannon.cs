using Exa.Grids.Blocks.Components;
using Exa.Ships.Targeting;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Autocannon : Block, IBehaviourMarker<AutocannonData>
    {
        public AutocannonBehaviour turretBehaviour;

        BlockBehaviour<AutocannonData> IBehaviourMarker<AutocannonData>.Component => turretBehaviour;

        public override IEnumerable<BlockBehaviour> GetBehaviours() {
            return base.GetBehaviours()
                .Append(turretBehaviour);
        }

        public void ForceActive() {
            foreach (var behaviour in GetBehaviours()) {
                behaviour.ForceActive();
            }
        }
    }
}