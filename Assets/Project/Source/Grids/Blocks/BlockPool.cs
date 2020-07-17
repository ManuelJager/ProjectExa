using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;

namespace Exa.Grids
{
    public class BlockPool : Pool
    {
        public override bool Return(PoolMember poolMember)
        {
            var result = base.Return(poolMember);
            if (result)
            {
                var block = poolMember.gameObject.GetComponent<Block>();

                // Reset values
                block.anchoredBlueprintBlock.blueprintBlock.RuntimeContext.SetValues(block);
            }
            return result;
        }
    }
}