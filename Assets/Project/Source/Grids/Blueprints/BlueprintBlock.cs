using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints.Editor;
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

        public void SetupBehaviourInGrid(SpriteRenderer spriteRenderer, GameObject blockGO)
        {
            spriteRenderer.flipX = blueprintBlock.flippedX;
            spriteRenderer.flipY = blueprintBlock.flippedY;
            blockGO.transform.localRotation = blueprintBlock.QuaternionRotation;
            blockGO.transform.localPosition = ShipEditorUtils.GetRealPositionByAnchor(blueprintBlock, gridAnchor);
        }

        public GameObject CreateBehaviourInGrid(Transform parent)
        {
            var prefab = MainManager.Instance.blockFactory.GetBlock(blueprintBlock.id);
            var blockGO = GameObject.Instantiate(prefab, parent);
            var spriteRenderer = blockGO.GetComponent<SpriteRenderer>();
            SetupBehaviourInGrid(spriteRenderer, blockGO);
            return blockGO;
        }
    }

    [Serializable]
    public struct BlueprintBlock
    {
        public string id;
        [DefaultValue(false)] public bool flippedX;
        [DefaultValue(false)] public bool flippedY;

        [JsonIgnore]
        private int rotation;

        [JsonIgnore]
        private BlockTemplate runtimeContext;

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

        public void Rotate(int quarterTurns)
        {
            Rotation = quarterTurns % 4;
        }

        [JsonIgnore]
        public Quaternion QuaternionRotation
        {
            get => Quaternion.Euler(0, 0, Rotation * 90f);
        }
    }
}