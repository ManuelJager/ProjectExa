using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class GyroscopeBlock : Block, IPhysicalBlock, IGyroscopeBlock
    {
        public PhysicalBlockData PhysicalBlockData { get; set; }
        public GyroscopeBlockData GyroscopeBlockData { get; set; }
    }
}