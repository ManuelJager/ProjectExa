using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct GyroscopeData : IBlockComponentData
    {
        public float turningRate;

        public void AddGridTotals(GridTotals totals)
        {
            totals.TurningPower += turningRate;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.TurningPower -= turningRate;
        }
    }
}