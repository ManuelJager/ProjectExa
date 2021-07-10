using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes {
    public class Gyroscope : Block, IBehaviourMarker<GyroscopeData> {
        [SerializeField] private GyroscopeBehaviour gyroscopeBehaviour;

        BlockBehaviour<GyroscopeData> IBehaviourMarker<GyroscopeData>.Component {
            get => gyroscopeBehaviour;
        }

        public override IEnumerable<BlockBehaviour> GetBehaviours() {
            return base.GetBehaviours().Append(gyroscopeBehaviour);
        }
    }
}