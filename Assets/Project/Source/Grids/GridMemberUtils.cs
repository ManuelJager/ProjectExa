using Exa.Math;
using UnityEngine;

namespace Exa.Grids
{
    public static class GridMemberUtils
    {
        public static void SetupGameObject(this IGridMember gridMember, GameObject blockGO) {
            var blueprintBlock = gridMember.BlueprintBlock;
            var gridAnchor = gridMember.GridAnchor;

            blockGO.name = $"{blueprintBlock.Template.displayId} {gridAnchor}";
            var spriteRenderer = blockGO.GetComponent<SpriteRenderer>();
            gridMember.BlueprintBlock.SetSpriteRendererFlips(spriteRenderer);
            gridMember.UpdateLocals(blockGO);
        }

        public static void UpdateLocals(this IGridMember gridMember, GameObject blockGO) {
            var blueprintBlock = gridMember.BlueprintBlock;

            blockGO.transform.localRotation = blueprintBlock.GetRotation();
            blockGO.transform.localPosition = gridMember.GetLocalPosition();
        }

        public static Vector2 GetLocalPosition(this IGridMember gridMember) {
            var size = gridMember.BlueprintBlock.Template.size - Vector2Int.one;

            var offset = new Vector2 {
                x = size.x / 2f,
                y = size.y / 2f
            }.Rotate(gridMember.BlueprintBlock.Rotation);

            offset *= gridMember.BlueprintBlock.FlipVector;

            return offset + gridMember.GridAnchor;
        }
    }
}