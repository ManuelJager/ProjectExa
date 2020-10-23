using Exa.Grids.Blocks;

namespace Exa.Ships
{
    public readonly struct ShipMask
    {
        public int LayerMask { get; }
        public ShipContext ContextMask { get; }

        public ShipMask(ShipContext shipContext) {
            LayerMask = UnityEngine.LayerMask.GetMask("unit");
            ContextMask = shipContext;
        }
    }
}