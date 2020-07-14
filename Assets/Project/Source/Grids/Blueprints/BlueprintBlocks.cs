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
        [JsonIgnore] public LazyCache<Vector2> CentreOfMass { get; }
        [JsonIgnore] public List<AnchoredBlueprintBlock> AnchoredBlueprintBlocks { get; private set; } = new List<AnchoredBlueprintBlock>();
        [JsonIgnore] public BlueprintBlocksOccupiedTilesCache OccupiedTiles { get; private set; } = new BlueprintBlocksOccupiedTilesCache();

        public BlueprintBlocks()
            : base()
        {
            Size = new LazyCache<Vector2Int>(() =>
            {
                var bounds = new GridBounds(OccupiedTiles.Keys);
                return bounds.GetDelta();
            });

            CentreOfMass = new LazyCache<Vector2>(() =>
            {
                return CalculateCentreOfMass();
            });
        }

        public void Add(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            Size.Invalidate();
            CentreOfMass.Invalidate();

            AnchoredBlueprintBlocks.Add(anchoredBlueprintBlock);

            // Get grid positions of blueprint block
            var tilePositions = GridUtils.GetOccupiedTilesByAnchor(anchoredBlueprintBlock);

            // Add neighbour references
            foreach (var neighbour in GetNeighbours(tilePositions))
            {
                neighbour.neighbours.Add(anchoredBlueprintBlock);
                anchoredBlueprintBlock.neighbours.Add(neighbour);
            }

            foreach (var tilePosition in tilePositions)
            {
                OccupiedTiles.Add(tilePosition, anchoredBlueprintBlock);
            }
        }

        public void Remove(Vector2Int key)
        {
            Size.Invalidate();
            CentreOfMass.Invalidate();

            var anchoredBlueprintBlock = GetAnchoredBlockAtGridPos(key);
            var tilePositions = GridUtils.GetOccupiedTilesByAnchor(anchoredBlueprintBlock);

            AnchoredBlueprintBlocks.Remove(anchoredBlueprintBlock);

            // Remove neighbour references
            foreach (var neighbour in anchoredBlueprintBlock.neighbours)
            {
                neighbour.neighbours.Remove(anchoredBlueprintBlock);
            }

            foreach (var occupiedTile in tilePositions)
            {
                OccupiedTiles.Remove(occupiedTile);
            }
        }

        public bool HasOverlap(Vector2Int gridPosition)
        {
            return OccupiedTiles.ContainsKey(gridPosition);
        }

        public bool HasOverlap(IEnumerable<Vector2Int> gridPositions)
        {
            return OccupiedTiles
                .Select((item) => item.Key)
                .Intersect(gridPositions)
                .Any();
        }

        public bool ContainsBlockAtGridPos(Vector2Int gridPos)
        {
            return OccupiedTiles.ContainsKey(gridPos);
        }

        public AnchoredBlueprintBlock GetAnchoredBlockAtGridPos(Vector2Int gridPos)
        {
            return OccupiedTiles[gridPos];
        }

        public BlueprintBlock GetBlockAtGridPos(Vector2Int gridPos)
        {
            return OccupiedTiles[gridPos].blueprintBlock;
        }

        public Vector2Int GetGridAnchorByGridPos(Vector2Int gridPos)
        {
            return OccupiedTiles[gridPos].gridAnchor;
        }

        public BlueprintBlocks Clone()
        {
            var newBlocks = new BlueprintBlocks();
            foreach (var block in AnchoredBlueprintBlocks)
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
                if (OccupiedTiles.ContainsKey(neighbourPosition))
                {
                    var neighbour = OccupiedTiles[neighbourPosition];
                    if (!neighbours.Contains(neighbour))
                    {
                        neighbours.Add(neighbour);
                    }
                }
            }

            return neighbours;
        }

        private Vector2 CalculateCentreOfMass()
        {
            var total = new Vector2();

            foreach (var block in AnchoredBlueprintBlocks)
            {
                total += block.GetRealPosition();
            }

            return total / AnchoredBlueprintBlocks.Count;
        }
    }
}