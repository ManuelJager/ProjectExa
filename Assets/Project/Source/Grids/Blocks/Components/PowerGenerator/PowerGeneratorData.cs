namespace Exa.Grids.Blocks.Components
{
    public struct PowerGeneratorData : IBlockComponentData
    {
        public float powerGeneration;

        public void AddGridTotals(GridTotals totals)
        {
            totals.PeakPowerGeneration += powerGeneration;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.PeakPowerGeneration -= powerGeneration;
        }
    }
}