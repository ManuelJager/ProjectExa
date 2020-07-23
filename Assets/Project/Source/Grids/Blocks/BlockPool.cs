using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;

namespace Exa.Grids.Blocks
{
    public class BlockPool : Pool<BlockPoolMember>
    {
        public override bool Return(PoolMember poolMember)
        {
            var result = base.Return(poolMember);
            if (result)
            {
                var block = ((BlockPoolMember)poolMember).block;

                // Reset values
                block.anchoredBlueprintBlock.blueprintBlock.RuntimeContext.SetValues(block);
            }
            return result;
        }

        protected override BlockPoolMember InstantiatePrefab()
        {
            var member = base.InstantiatePrefab();
            member.block = member.gameObject.GetComponent<Block>();
            return member;
        }
    }
}