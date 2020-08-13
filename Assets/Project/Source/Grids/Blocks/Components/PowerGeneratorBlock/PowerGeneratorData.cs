namespace Exa.Grids.Blocks.Components
{
    public struct PowerGeneratorData : IBlockComponentData
    {
        public float peakGeneration;

        public void AddGridTotals(GridTotals totals)
        {
            totals.PeakPowerGeneration += peakGeneration;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.PeakPowerGeneration -= peakGeneration;
        }
    }
}