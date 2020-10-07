using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Gyroscope : Block, IBehaviourMarker<GyroscopeData>
    {
        [SerializeField] private GyroscopeBehaviour gyroscopeBehaviour;

        BlockBehaviour<GyroscopeData> IBehaviourMarker<GyroscopeData>.Component => gyroscopeBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(gyroscopeBehaviour);
        }
    }
}