using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PowerGenerator : Block, IPhysicalBlock, IPowerGeneratorBlock
    {
        public PhysicalBlockData PhysicalBlockData { get; set; }
        public PowerGeneratorBlockData PowerGeneratorBlockData { get; set; }
    }
}