using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes {
    public class Thruster : Block, IBehaviourMarker<ThrusterData> {
        [SerializeField] private ThrusterBehaviour thrusterBehaviour;

        BlockBehaviour<ThrusterData> IBehaviourMarker<ThrusterData>.Component {
            get => thrusterBehaviour;
        }

        public override IEnumerable<BlockBehaviour> GetBehaviours() {
            return base.GetBehaviours()
                .Append(thrusterBehaviour);
        }
    }
}