using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class GyroscopeBlock : Block, IPhysicalBlock, IGyroscopeBlock
    {
        public PhysicalBlockData PhysicalBlockData { get; set; }
        public GyroscopeBlockData GyroscopeBlockData { get; set; }


    }
}
