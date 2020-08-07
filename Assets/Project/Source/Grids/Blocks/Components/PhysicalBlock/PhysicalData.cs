using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct PhysicalData : IBlockComponentData
    {
        public float hull;
        public float armor;
        public short mass;
    }
}