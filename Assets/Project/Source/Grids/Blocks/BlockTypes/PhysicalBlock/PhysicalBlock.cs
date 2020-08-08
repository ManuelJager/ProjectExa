using Exa.Grids.Blocks.Components;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PhysicalBlock : Block, IPhysical
    {
        [SerializeField] private PhysicalBehaviour physicalBehaviour;

        public PhysicalBehaviour PhysicalBehaviour { get => physicalBehaviour; set => physicalBehaviour = value; }

        public override Ship Ship
        {
            set
            {
                base.Ship = value;
                physicalBehaviour.Ship = value;
            }
        }
    }
}