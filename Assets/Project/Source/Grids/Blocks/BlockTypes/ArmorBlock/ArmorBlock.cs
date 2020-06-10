using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class ArmorBlock : Block, IPhysicalBlock
    {
        public PhysicalBlockData physicalBlockData { get; set; }
    }
}