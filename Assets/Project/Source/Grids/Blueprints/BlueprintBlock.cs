using Boo.Lang;
using Exa.Generics;
using Exa.Grids.Blocks;
using Newtonsoft.Json;
using System;
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
    }

    [Serializable]
    public struct BlueprintBlock
    {
        public string id;
        public bool flippedX;
        public bool flippedY;

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
                    runtimeContext = GameManager.Instance.blockFactory.blockTemplatesDict[id];
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