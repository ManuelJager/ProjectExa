﻿using Exa.Generics;
using Exa.IO.Json;
using Newtonsoft.Json;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintBlocks : Grid<AnchoredBlueprintBlock>, ICloneable<BlueprintBlocks>
    {
        public float MaxSize
        {
            get
            {
                var size = Size.Value;
                return Mathf.Max(size.x, size.y);
            }
        }

        public BlueprintBlocks Clone()
        {
            var newBlocks = new BlueprintBlocks();
            foreach (var block in GridMembers)
            {
                newBlocks.Add(block.Clone());
            }
            return newBlocks;
        }
    }
}