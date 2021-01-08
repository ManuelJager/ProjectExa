using Exa.Grids.Blocks;
using Exa.Utils;

namespace Exa.Ships
{
    public readonly struct ContextMask
    {
        public int LayerMask { get; }
        private BlockContext BlockContextMask { get; }

        internal ContextMask(BlockContext blockContextMask, int layerMask) {
            LayerMask = layerMask;
            BlockContextMask = blockContextMask;
        }

        public bool HasValue(BlockContext context) {
            return BlockContextMask.HasValue(context);
        }
    }

    public static class ContextMaskUtils
    {
        public static ContextMask GetShipMask(this BlockContext context) {
            return new ContextMask(context, UnityEngine.LayerMask.GetMask("unit"));
        }

        public static ContextMask GetBlockMask(this BlockContext context) {
            return new ContextMask(context, UnityEngine.LayerMask.GetMask("blocks"));
        }
    }
}