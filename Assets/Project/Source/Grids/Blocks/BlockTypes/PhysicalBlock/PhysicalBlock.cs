using Exa.Grids.Blocks.Components;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PhysicalBlock : Block
    {
        [SerializeField] private PhysicalBehaviour physicalBlockBehaviour;

        public PhysicalBehaviour PhysicalBlockBehaviour { get => physicalBlockBehaviour; set => physicalBlockBehaviour = value; }

        public override Ship Ship
        {
            set
            {
                base.Ship = value;
                physicalBlockBehaviour.Ship = value;
            }
        }
    }
}