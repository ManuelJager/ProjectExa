using Exa.Generics;
using Exa.Grids.Blueprints.BlueprintEditor;
using Exa.IO.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintBlocks : Dictionary<Vector2Int, BlueprintBlock>, ICloneable<BlueprintBlocks>
    {
        [JsonIgnore]
        internal BlueprintBlocksOccupiedTilesCache occupiedTiles = new BlueprintBlocksOccupiedTilesCache();

        public BlueprintBlocks()
            : base()
        {
        }

        public BlueprintBlocks(BlueprintBlocks dictionary)
            : base(dictionary)
        {
        }

        public new void Add(Vector2Int key, BlueprintBlock value)
        {
            base.Add(key, value);

            var anchoredBlueprintBlock = new AnchoredBlueprintBlock
            {
                gridAnchor = key,
                blueprintBlock = value
            };

            foreach (var occupiedTile in ShipEditorUtils.GetOccupiedTilesByAnchor(value, key))
            {
                occupiedTiles.Add(occupiedTile, anchoredBlueprintBlock);
            }
        }

        public new void Remove(Vector2Int key)
        {
            foreach (var occupiedTile in ShipEditorUtils.GetOccupiedTilesByAnchor(base[key], key))
            {
                occupiedTiles.Remove(occupiedTile);
            }
            base.Remove(key);
        }

        public bool HasOverlap(Vector2Int gridPosition)
        {
            return occupiedTiles.ContainsKey(gridPosition);
        }

        public bool HasOverlap(IEnumerable<Vector2Int> gridPositions)
        {
            return occupiedTiles
                .Select((item) => item.Key)
                .Intersect(gridPositions)
                .Any();
        }

        public bool HasOverlap(BlueprintBlock block, Vector2Int gridAnchor)
        {
            var gridPositions = ShipEditorUtils.GetOccupiedTilesByAnchor(block, gridAnchor);

            return occupiedTiles
                .Select((item) => item.Key)
                .Intersect(gridPositions)
                .Any();
        }

        public AnchoredBlueprintBlock GetAnchoredBlockAtGridPos(Vector2Int gridPos)
        {
            return occupiedTiles[gridPos];
        }

        public BlueprintBlock GetBlockAtGridPos(Vector2Int gridPos)
        {
            return occupiedTiles[gridPos].blueprintBlock;
        }

        public Vector2Int GetGridAnchorByGridPos(Vector2Int gridPos)
        {
            return occupiedTiles[gridPos].gridAnchor;
        }

        public BlueprintBlocks Clone()
        {
            return new BlueprintBlocks(this)
            {
                occupiedTiles = occupiedTiles.Clone()
            };
        }
    }
}