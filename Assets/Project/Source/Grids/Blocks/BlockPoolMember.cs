using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;

namespace Exa.Grids.Blocks
{
    public class BlockPoolMember : PoolMember
    {
        public Block block;

        protected override void OnDisable() { }

        public void ReturnBlock() {
            base.OnDisable();
        }

        protected override bool IgnoreClause() => GameSystems.IsQuitting;
    }
}