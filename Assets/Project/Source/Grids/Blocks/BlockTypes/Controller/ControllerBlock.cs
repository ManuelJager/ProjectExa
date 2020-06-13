using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class ControllerBlock : Block, IPhysicalBlock
    {
        public PhysicalBlockData PhysicalBlockData { get; set; }
    }
}