using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class AnchoredBlueprintBlock : ICloneable<AnchoredBlueprintBlock>
    {
        public Vector2Int gridAnchor;
        public BlueprintBlock blueprintBlock;
        public List<AnchoredBlueprintBlock> neighbours = new List<AnchoredBlueprintBlock>();

        // NOTE: Doesn't clone neighbours
        public AnchoredBlueprintBlock Clone()
        {
            return new AnchoredBlueprintBlock
            {
                gridAnchor = gridAnchor,
                blueprintBlock = blueprintBlock,
            };
        }

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

        public GameObject CreateBehaviourInGrid(Transform parent)
        {
            var prefab = MainManager.Instance.blockFactory.GetBlock(blueprintBlock.id);
            var blockGO = GameObject.Instantiate(prefab, parent);
            var spriteRenderer = blockGO.GetComponent<SpriteRenderer>();
            UpdateSpriteRenderer(spriteRenderer);
            UpdateLocals(blockGO);
            return blockGO;
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

    [Serializable]
    public struct BlueprintBlock
    {
        public string id;
        [DefaultValue(false)] public bool flippedX;
        [DefaultValue(false)] public bool flippedY;

        [JsonIgnore] private int rotation;
        [JsonIgnore] private BlockTemplate runtimeContext;

        public int Rotation
        {
            get => rotation;
            set
            {
                rotation = value % 4;
            }
        }

        [JsonIgnore]
        public BlockTemplate RuntimeContext
        {
            get
            {
                if (runtimeContext == null)
                {
                    runtimeContext = MainManager.Instance.blockFactory.blockTemplatesDict[id];
                }
                return runtimeContext;
            }
        }

        [JsonIgnore]
        public Quaternion QuaternionRotation
        {
            get => Quaternion.Euler(0, 0, Rotation * 90f);
        }

        public Vector2Int CalculateSizeDelta()
        {
            var area = RuntimeContext.size.Rotate(Rotation);

            if (flippedX) area.x = -area.x;
            if (flippedY) area.y = -area.y;

            return area;
        }
    }
}