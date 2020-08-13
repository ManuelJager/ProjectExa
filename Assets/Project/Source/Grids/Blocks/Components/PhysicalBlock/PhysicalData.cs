using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct PhysicalData : IBlockComponentData
    {
        public float hull;
        public float armor;
        public short mass;

        public void AddGridTotals(GridTotals totals)
        {
            totals.Mass += mass;
            totals.Hull += hull;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.Mass -= mass;
            totals.Hull -= hull;
        }
    }
}