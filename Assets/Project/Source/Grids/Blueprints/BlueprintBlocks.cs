using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.IO.Json;
using Newtonsoft.Json;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintBlocks : Grid<ABpBlock>, ICloneable<BlueprintBlocks>
    {
        public GridTotals GetTotals(BlockContext context = BlockContext.DefaultGroup) {
            return Systems.TotalsManager.GetGridTotalsSafe(this, context);
        }
        
        public BlueprintBlocks Clone() {
            var newBlocks = new BlueprintBlocks();
            foreach (var block in GridMembers) {
                newBlocks.Add(block.Clone());
            }

            return newBlocks;
        }
    }
}