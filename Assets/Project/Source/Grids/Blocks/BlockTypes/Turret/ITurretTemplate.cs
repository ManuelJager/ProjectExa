using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public interface ITurretTemplate
    {
        public ITurretValues GetTurretValues(BlockContext context);
    }
}