using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using System;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    public class AnchoredBlueprintBlock : ICloneable<AnchoredBlueprintBlock>
    {
        public Vector2Int gridAnchor;
        public BlueprintBlock blueprintBlock;

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

        public GameObject CreateInertBehaviourInGrid(Transform parent)
        {
            var blockGO = MainManager.Instance.blockFactory.GetInertBlock(blueprintBlock.id, parent);
            SetupGameObject(blockGO);
            return blockGO;
        }

        public Block CreateBehaviourInGrid(Transform parent, BlockPrefabType blockPrefabType)
        {
            var block = MainManager.Instance.blockFactory.GetBlock(blueprintBlock.id, parent, blockPrefabType);
            block.anchoredBlueprintBlock = this;
            SetupGameObject(block.gameObject);
            return block;
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

        private void SetupGameObject(GameObject blockGO)
        {
            blockGO.name = $"{blueprintBlock.RuntimeContext.displayId} {gridAnchor}";
            var spriteRenderer = blockGO.GetComponent<SpriteRenderer>();
            UpdateSpriteRenderer(spriteRenderer);
            UpdateLocals(blockGO);
        }
    }
}