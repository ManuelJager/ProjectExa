using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;

namespace Exa.Grids.Blocks {
    public class BlockPool : Pool<BlockPoolMember> {
        public BlockContext blockContext;
        public BlockTemplate blockTemplate;

        public override BlockPoolMember Retrieve() {
            var member = base.Retrieve();
            var block = member.block;
            S.Blocks.Values.SetValues(blockContext, blockTemplate, block);

            return member;
        }

        protected override BlockPoolMember InstantiatePrefab() {
            var member = base.InstantiatePrefab();
            var block = member.gameObject.GetComponent<Block>();
            block.blockPoolMember = member;
            member.block = block;

            return member;
        }
    }
}