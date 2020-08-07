using Exa.Grids.Blocks.Components;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Gyroscope : PhysicalBlock
    {
        [SerializeField] private GyroscopeBehaviour gyroscopeBehaviour;

        public GyroscopeBehaviour GyroscopeBehaviour { get => gyroscopeBehaviour; set => gyroscopeBehaviour = value; }

        public override Ship Ship
        {
            set
            {
                base.Ship = value;
                gyroscopeBehaviour.Ship = value;
            }
        }
    }
}