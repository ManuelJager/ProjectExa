using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids
{
    public interface IGridMember : IGridTotalsModifier
    {
        Vector2Int GridAnchor { get; }
        BlueprintBlock BlueprintBlock { get; }
    }

    public static class IGridMemberExtensions
    {
        public static bool GetIsController(this IGridMember gridMember) {
            return gridMember.GetMemberCategory().HasValue(BlockCategory.ShipController);
        }

        public static BlockCategory GetMemberCategory(this IGridMember gridMember) {
            return gridMember.BlueprintBlock.Template.category;
        }
    }
}