using Exa.Data;
using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct GyroscopeData : IBlockComponentData
    {
        public Percentage turningRate;

        public void AddGridTotals(GridTotals totals)
        {
            totals.TurningPowerModifier += turningRate;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.TurningPowerModifier -= turningRate;
        }
    }
}