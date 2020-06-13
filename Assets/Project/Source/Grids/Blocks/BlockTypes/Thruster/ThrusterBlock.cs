using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class ThrusterBlock : Block, IPhysicalBlock, IThrusterBlock
    {
        public PhysicalBlockData PhysicalBlockData { get; set; }
        public ThrusterBlockData ThrusterBlockData { get; set; }
    }
}