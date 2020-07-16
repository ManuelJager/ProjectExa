using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Gyroscope : PhysicalBlock
    {
        [SerializeField] private GyroscopeBehaviour gyroscopeBehaviour;

        public GyroscopeBehaviour GyroscopeBehaviour { get => gyroscopeBehaviour; set => gyroscopeBehaviour = value; }
    }
}