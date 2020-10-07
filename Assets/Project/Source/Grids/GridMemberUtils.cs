using Exa.Math;
using UnityEngine;

namespace Exa.Grids
{
    public static class GridMemberUtils
    {
        public static void SetupGameObject(this IGridMember gridMember, GameObject blockGo)
        {
            var blueprintBlock = gridMember.BlueprintBlock;
            var gridAnchor = gridMember.GridAnchor;

            blockGo.name = $"{blueprintBlock.Template.displayId} {gridAnchor}";
            var spriteRenderer = blockGo.GetComponent<SpriteRenderer>();
            gridMember.BlueprintBlock.SetSpriteRendererFlips(spriteRenderer);
            gridMember.UpdateLocals(blockGo);
        }

        public static void UpdateLocals(this IGridMember gridMember, GameObject blockGo)
        {
            var blueprintBlock = gridMember.BlueprintBlock;

            blockGo.transform.localRotation = blueprintBlock.QuaternionRotation;
            blockGo.transform.localPosition = gridMember.GetLocalPosition();
        }

        public static Vector2 GetLocalPosition(this IGridMember gridMember)
        {
            var size = gridMember.BlueprintBlock.Template.size - Vector2Int.one;

            var offset = new Vector2
            {
                x = size.x / 2f,
                y = size.y / 2f
            }.Rotate(gridMember.BlueprintBlock.Rotation);

            offset *= gridMember.BlueprintBlock.FlipVector;

            return offset + gridMember.GridAnchor;
        }
    }
}