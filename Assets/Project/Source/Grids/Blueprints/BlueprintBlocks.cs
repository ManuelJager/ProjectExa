using Exa.Generics;
using Exa.IO.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintBlocks : ICloneable<BlueprintBlocks>, IGrid<AnchoredBlueprintBlock>
    {
        [JsonIgnore] public LazyCache<Vector2Int> Size { get; }
        [JsonIgnore] public LazyCache<Vector2> CentreOfMass { get; }
        [JsonIgnore] public List<AnchoredBlueprintBlock> GridMembers { get; private set; } = new List<AnchoredBlueprintBlock>();
        [JsonIgnore] public Dictionary<Vector2Int, AnchoredBlueprintBlock> OccupiedTiles { get; private set; } = new BlueprintBlocksOccupiedTilesCache();
        [JsonIgnore] public Dictionary<AnchoredBlueprintBlock, List<AnchoredBlueprintBlock>> NeighbourDict { get; private set; } = new Dictionary<AnchoredBlueprintBlock, List<AnchoredBlueprintBlock>>();

        public BlueprintBlocks()
            : base()
        {
            Size = new LazyCache<Vector2Int>(() =>
            {
                var bounds = new GridBounds(OccupiedTiles.Keys);
                return bounds.GetDelta();
            });

            CentreOfMass = new LazyCache<Vector2>(this.CalculateCentreOfMass);
        }

        public BlueprintBlocks Clone()
        {
            var newBlocks = new BlueprintBlocks();
            foreach (var block in GridMembers)
            {
                newBlocks.Add(block);
            }
            return newBlocks;
        }
    }
}