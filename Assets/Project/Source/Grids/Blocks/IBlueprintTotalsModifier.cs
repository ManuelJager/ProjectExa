using Exa.Grids.Blueprints;

namespace Exa.Grids.Blocks
{
    public interface IBlueprintTotalsModifier
    {
        void AddBlueprintTotals(Blueprint blueprint);

        void RemoveBlueprintTotals(Blueprint blueprint);
    }
}