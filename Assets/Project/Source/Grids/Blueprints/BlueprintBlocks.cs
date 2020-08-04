﻿using Exa.Generics;
using Exa.IO.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintBlocks : Grid<AnchoredBlueprintBlock>, ICloneable<BlueprintBlocks>
    {
        public BlueprintBlocks Clone()
        {
            var newBlocks = new BlueprintBlocks();
            foreach (var block in this)
            {
                newBlocks.Add(block);
            }
            return newBlocks;
        }
    }
}