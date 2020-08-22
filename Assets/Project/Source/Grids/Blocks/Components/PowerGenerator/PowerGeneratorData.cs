using Exa.Data;

namespace Exa.Grids.Blocks.Components
{
    public struct PowerGeneratorData : IBlockComponentData
    {
        public Percentage powerGeneration;

        public void AddGridTotals(GridTotals totals)
        {
            totals.PowerGenerationModifier += powerGeneration;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            totals.PowerGenerationModifier -= powerGeneration;
        }
    }
}