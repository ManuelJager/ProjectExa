using Exa.Ships;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Gyroscope : Block, IGyroscope
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