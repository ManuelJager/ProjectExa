using Exa.Ships;
using Exa.Grids.Blocks.Components;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Gyroscope : Block, IGyroscope
    {
        [SerializeField] private GyroscopeBehaviour gyroscopeBehaviour;

        BlockBehaviour<GyroscopeData> IBehaviourMarker<GyroscopeData>.Component
        {
            get => gyroscopeBehaviour; 
            set => gyroscopeBehaviour = value as GyroscopeBehaviour;
        }

        protected override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(gyroscopeBehaviour);
        }
    }
}