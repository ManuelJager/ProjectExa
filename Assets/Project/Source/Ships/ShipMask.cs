using Exa.Grids.Blocks;

namespace Exa.Ships
{
    public struct ShipMask
    {
        public int LayerMask { get; private set; }
        public ShipContext ContextMask { get; private set; }

        public ShipMask(ShipContext shipContext)
        {
            LayerMask = UnityEngine.LayerMask.GetMask("unit");
            ContextMask = shipContext;
        }
    }
}