using Exa.Grids.Blocks;
using Exa.Utils;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
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