using Exa.Data;

namespace Exa.Grids.Blocks.Components
{
    public struct PowerConsumerData : IBlockComponentData
    {
        public Percentage powerConsumption;

        public void AddGridTotals(GridTotals totals)
        {
            totals.PowerConsumptionModifier += powerConsumption;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.PowerConsumptionModifier -= powerConsumption;
        }
    }
}