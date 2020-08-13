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

        public GyroscopeBehaviour GyroscopeBehaviour 
        {
            get => gyroscopeBehaviour; 
            set => gyroscopeBehaviour = value;
        }

        protected override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(gyroscopeBehaviour);
        }
    }
}