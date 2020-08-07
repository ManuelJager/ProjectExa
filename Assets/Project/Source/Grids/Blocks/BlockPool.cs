using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;

namespace Exa.Grids.Blocks
{
    public class BlockPool : Pool<BlockPoolMember>
    {
        public BlockTemplate blockTemplate;

        public override BlockPoolMember Retrieve()
        {
            var member = base.Retrieve();
            var block = member.block;
            blockTemplate.SetValues(block);
            return member;
        }

        protected override BlockPoolMember InstantiatePrefab()
        {
            var member = base.InstantiatePrefab();
            member.block = member.gameObject.GetComponent<Block>();
            return member;
        }
    }
}