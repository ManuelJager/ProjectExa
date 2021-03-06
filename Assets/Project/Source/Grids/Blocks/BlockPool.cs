﻿using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;

namespace Exa.Grids.Blocks
{
    public class BlockPool : Pool<BlockPoolMember>
    {
        public BlockContext blockContext;
        public BlockTemplate blockTemplate;

        public override BlockPoolMember Retrieve() {
            var member = base.Retrieve();
            var block = member.block;
            Systems.Blocks.valuesStore.SetValues(blockContext, blockTemplate, block);
            return member;
        }

        protected override BlockPoolMember InstantiatePrefab() {
            var member = base.InstantiatePrefab();
            member.block = member.gameObject.GetComponent<Block>();
            return member;
        }
    }
}