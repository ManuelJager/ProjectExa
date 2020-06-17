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

            // Get grid positions of blueprint block
            var tilePositions = ShipEditorUtils.GetOccupiedTilesByAnchor(anchoredBlueprintBlock);

            // Add neighbour references
            foreach (var neighbour in GetNeighbours(tilePositions))
            {
                neighbour.neighbours.Add(anchoredBlueprintBlock);
                anchoredBlueprintBlock.neighbours.Add(neighbour);
            }

            foreach (var tilePosition in tilePositions)
            {
                occupiedTiles.Add(tilePosition, anchoredBlueprintBlock);
            }
        }

        public IEnumerable<AnchoredBlueprintBlock> GetNeighbours(IEnumerable<Vector2Int> tilePositions)
        {
            // Get grid positions around block
            var bounds = new Generics.Bounds(tilePositions);
            var neighbourPositions = bounds.GetAdjacentPositions();

            var neighbours = new List<AnchoredBlueprintBlock>();

            foreach (var neighbourPosition in neighbourPositions)
            {
                if (occupiedTiles.ContainsKey(neighbourPosition))
                {
                    var neighbour = occupiedTiles[neighbourPosition];
                    if (!neighbours.Contains(neighbour))
                    {
                        neighbours.Add(neighbour);
                    }
                }
            }

            return neighbours;
        }

        public new void Remove(Vector2Int key)
        {
            var tilePositions = ShipEditorUtils.GetOccupiedTilesByAnchor(base[key], key);
            var anchoredBlueprintBlock = occupiedTiles[key];

            // Add neighbour references
            foreach (var neighbour in anchoredBlueprintBlock.neighbours)
            {
                neighbour.neighbours.Remove(anchoredBlueprintBlock);
            }

            foreach (var occupiedTile in tilePositions)
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
            var blocks = new BlueprintBlocks();
            foreach (var kvp in this)
            {
                blocks.Add(kvp.Key, kvp.Value);
            }
            return blocks;
        }
    }
}