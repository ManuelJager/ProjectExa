using Exa.Grids.Blocks;
using Exa.Math;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public int Rotation
        {
            get
            {
                var remainder = rotation % 4;
                if (remainder < 0)
                {
                    remainder += 4;
                }
                return remainder;
            }
            set => rotation = value;
        }

        [JsonIgnore]
        public Vector2 FlipVector => new Vector2
        {
            x = flippedX ? -1 : 1,
            y = flippedY ? -1 : 1
        };

        [JsonIgnore]
        public BlockTemplate Template
        {
            get
            {
                if (!Systems.Blocks.blockTemplatesDict.ContainsKey(id))
                {
                    throw new KeyNotFoundException($"Block template with id: {id} doesn't exist");
                }

                return Systems.Blocks.blockTemplatesDict[id];
            }
        }

        [JsonIgnore]
        public Quaternion QuaternionRotation
        {
            get => Quaternion.Euler(0, 0, Rotation * 90f);
        }

        public Vector2Int CalculateSizeDelta()
        {
            var area = Template.size.Rotate(Rotation);

            if (flippedX) area.x = -area.x;
            if (flippedY) area.y = -area.y;

            return area;
        }

        public void SetSpriteRendererFlips(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.flipX = Rotation % 2 == 0
                ? flippedX
                : flippedY;

            spriteRenderer.flipY = Rotation % 2 == 0
                ? flippedY
                : flippedX;
        }
    }
}