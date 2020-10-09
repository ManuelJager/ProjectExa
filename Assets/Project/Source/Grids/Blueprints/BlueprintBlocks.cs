using Exa.Generics;
using Exa.IO.Json;
using Newtonsoft.Json;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintBlocks : Grid<AnchoredBlueprintBlock>, ICloneable<BlueprintBlocks>
    {
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