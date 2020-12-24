using System.Collections.Generic;
using Exa.Grids.Blocks.BlockTypes;

namespace Exa.Gameplay
{
    public class Cluster : List<Block>
    {
        public bool containsController = false;
    }
}