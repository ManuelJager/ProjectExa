using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids
{
    public class BlockGrid : Grid<Block>
    {
        private Transform container;

        public BlockGrid(Transform container)
        {
            this.container = container;
        }
    }
}