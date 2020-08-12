using Exa.Math;
using UnityEngine;

namespace Exa.Grids
{
    public static class GridMemberUtils
    {
        public static void SetupGameObject(this IGridMember gridMember, GameObject blockGO)
        {
            var blueprintBlock = gridMember.BlueprintBlock;
            var gridAnchor = gridMember.GridAnchor;

            blockGO.name = $"{blueprintBlock.RuntimeContext.displayId} {gridAnchor}";
            var spriteRenderer = blockGO.GetComponent<SpriteRenderer>();
            gridMember.UpdateSpriteRenderer(spriteRenderer);
            gridMember.UpdateLocals(blockGO);
        }

        public static void UpdateSpriteRenderer(this IGridMember gridMember, SpriteRenderer spriteRenderer)
        {
            var blueprintBlock = gridMember.BlueprintBlock;

            spriteRenderer.flipX = blueprintBlock.Rotation % 2 == 0
                ? blueprintBlock.flippedX
                : blueprintBlock.flippedY;

            spriteRenderer.flipY = blueprintBlock.Rotation % 2 == 0
                ? blueprintBlock.flippedY
                : blueprintBlock.flippedX;
        }

        public static void UpdateLocals(this IGridMember gridMember, GameObject blockGO)
        {
            var blueprintBlock = gridMember.BlueprintBlock;

            blockGO.transform.localRotation = blueprintBlock.QuaternionRotation;
            blockGO.transform.localPosition = gridMember.GetLocalPosition();
        }

        public static Vector2 GetLocalPosition(this IGridMember gridMember)
        {
            var size = gridMember.BlueprintBlock.RuntimeContext.size - Vector2Int.one;

            var offset = new Vector2
            {
                x = size.x / 2f,
                y = size.y / 2f
            }.Rotate(gridMember.BlueprintBlock.Rotation);

            if (gridMember.BlueprintBlock.flippedX) offset.x = -offset.x;
            if (gridMember.BlueprintBlock.flippedY) offset.y = -offset.y;

            return offset + gridMember.GridAnchor;
        }
    }
}