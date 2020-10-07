using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Gyroscope : Block, IBehaviourMarker<GyroscopeData>
    {
        [SerializeField] private GyroscopeBehaviour _gyroscopeBehaviour;

        BlockBehaviour<GyroscopeData> IBehaviourMarker<GyroscopeData>.Component => _gyroscopeBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(_gyroscopeBehaviour);
        }
    }
}