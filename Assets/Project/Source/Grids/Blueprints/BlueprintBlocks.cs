using Exa.Generics;
using Exa.Grids.Blueprints.Editor;
using Exa.IO.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintBlocks : ICloneable<BlueprintBlocks>
    {
        [JsonIgnore] public LazyCache<Vector2Int> Size { get; }
        [JsonIgnore] public List<AnchoredBlueprintBlock> anchoredBlueprintBlocks = new List<AnchoredBlueprintBlock>();

        [JsonIgnore] private BlueprintBlocksOccupiedTilesCache occupiedTiles = new BlueprintBlocksOccupiedTilesCache();

        public BlueprintBlocks()
            : base()
        {
            Size = new LazyCache<Vector2Int>(() =>
            {
                var bounds = new GridBounds(occupiedTiles.Keys);
                return bounds.GetDelta();
            });
        }

        public void Add(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            Size.Invalidate();

            anchoredBlueprintBlocks.Add(anchoredBlueprintBlock);

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

        public void Remove(Vector2Int key)
        {
            Size.Invalidate();

            var anchoredBlueprintBlock = GetAnchoredBlockAtGridPos(key);
            var tilePositions = ShipEditorUtils.GetOccupiedTilesByAnchor(anchoredBlueprintBlock);

            anchoredBlueprintBlocks.Remove(anchoredBlueprintBlock);

            // Remove neighbour references
            foreach (var neighbour in anchoredBlueprintBlock.neighbours)
            {
                neighbour.neighbours.Remove(anchoredBlueprintBlock);
            }

            foreach (var occupiedTile in tilePositions)
            {
                occupiedTiles.Remove(occupiedTile);
            }
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
            var newBlocks = new BlueprintBlocks();
            foreach (var block in anchoredBlueprintBlocks)
            {
                newBlocks.Add(block);
            }
            return newBlocks;
        }

        private IEnumerable<AnchoredBlueprintBlock> GetNeighbours(IEnumerable<Vector2Int> tilePositions)
        {
            // Get grid positions around block
            var bounds = new GridBounds(tilePositions);
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
    }
}