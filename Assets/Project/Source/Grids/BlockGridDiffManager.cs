﻿using System.Collections.Generic;
using Exa.Grids.Blueprints;
using Exa.Ships;

namespace Project.Source.Grids
{
    public class BlockGridDiffManager
    {
        private Dictionary<BlockGrid, BlockGridDiff> diffs;

        public BlockGridDiffManager() {
            diffs = new Dictionary<BlockGrid, BlockGridDiff>();
        }

        public BlockGridDiff StartWatching(BlockGrid source, BlueprintGrid target) {
            var diff = new BlockGridDiff(source, target);
            diffs.Add(source, diff);
            return diff;
        }

        public void StopWatching(BlockGrid source) {
            diffs.Remove(source);
        }
        
        public BlockGridDiff GetDiff(BlockGrid blockGrid) {
            return diffs[blockGrid];
        }
    }
}