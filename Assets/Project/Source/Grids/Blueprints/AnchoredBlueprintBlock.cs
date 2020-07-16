using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class AnchoredBlueprintBlock : ICloneable<AnchoredBlueprintBlock>
    {
        public Vector2Int gridAnchor;
        public BlueprintBlock blueprintBlock;

        // TODO: Move this to the blueprint level
        public List<AnchoredBlueprintBlock> neighbours = new List<AnchoredBlueprintBlock>();

        public void UpdateSpriteRenderer(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.flipX = blueprintBlock.Rotation % 2 == 0 ? blueprintBlock.flippedX : blueprintBlock.flippedY;
            spriteRenderer.flipY = blueprintBlock.Rotation % 2 == 0 ? blueprintBlock.flippedY : blueprintBlock.flippedX;
        }

        public void UpdateLocals(GameObject blockGO)
        {
            blockGO.transform.localRotation = blueprintBlock.QuaternionRotation;
            blockGO.transform.localPosition = GetLocalPosition();
        }

        public GameObject CreateBehaviourInGrid(Transform parent, BlockPrefabType blockPrefabType)
        {
            var blockGO = MainManager.Instance.blockFactory.InstantiateBlock(blueprintBlock.id, parent, blockPrefabType);
            blockGO.name = $"{blueprintBlock.RuntimeContext.displayId} {gridAnchor}";
            var spriteRenderer = blockGO.GetComponent<SpriteRenderer>();
            UpdateSpriteRenderer(spriteRenderer);
            UpdateLocals(blockGO);
            return blockGO;
        }

        // NOTE: Doesn't clone neighbours
        public AnchoredBlueprintBlock Clone()
        {
            return new AnchoredBlueprintBlock
            {
                gridAnchor = gridAnchor,
                blueprintBlock = blueprintBlock,
            };
        }

        public Vector2 GetLocalPosition()
        {
            var size = blueprintBlock.RuntimeContext.size - Vector2Int.one;

            var offset = new Vector2
            {
                x = size.x / 2f,
                y = size.y / 2f
            }.Rotate(blueprintBlock.Rotation);

            if (blueprintBlock.flippedX) offset.x = -offset.x;
            if (blueprintBlock.flippedY) offset.y = -offset.y;

            return offset + gridAnchor;
        }
    }
}