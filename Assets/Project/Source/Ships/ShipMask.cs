using Exa.Grids.Blocks;

namespace Exa.Ships
{
    public readonly struct ShipMask
    {
        public int LayerMask { get; }
        public BlockContext ContextMask { get; }

        public ShipMask(BlockContext blockContext) {
            LayerMask = UnityEngine.LayerMask.GetMask("unit");
            ContextMask = blockContext;
        }
    }
}