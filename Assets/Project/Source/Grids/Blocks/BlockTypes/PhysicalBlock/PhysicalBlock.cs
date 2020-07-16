using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PhysicalBlock : Block
    {
        [SerializeField] private PhysicalBehaviour physicalBlockBehaviour;

        public PhysicalBehaviour PhysicalBlockBehaviour { get => physicalBlockBehaviour; set => physicalBlockBehaviour = value; }
    }
}