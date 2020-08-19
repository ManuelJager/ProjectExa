namespace Exa.Grids.Blocks.Components
{
    public struct PowerConsumerData : IBlockComponentData
    {
        public float powerConsumption;

        public void AddGridTotals(GridTotals totals)
        {
            totals.PeakPowerConsumption += powerConsumption;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.PeakPowerConsumption -= powerConsumption;
        }
    }
}