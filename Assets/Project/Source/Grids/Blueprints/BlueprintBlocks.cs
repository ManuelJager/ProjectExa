using Exa.Generics;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints.BlueprintEditor;
using Exa.IO.Json;
using Exa.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintBlocks : Dictionary<Vector2Int, BlueprintBlock>, ICloneable<BlueprintBlocks>
    {
        [JsonIgnore] internal BlueprintBlocksOccupiedTilesCache occupiedTiles = new BlueprintBlocksOccupiedTilesCache();

        [JsonIgnore] public LazyCache<Vector2Int> Size { get; }
        [JsonIgnore] public long Mass { get; private set; }
        [JsonIgnore] public float PeakPowerGeneration { get; private set; }
        

        public BlueprintBlocks()
            : base()
        {
            Size = new LazyCache<Vector2Int>(() =>
            {
                var bounds = new GridBounds(occupiedTiles.Keys);
                return bounds.GetDelta();
            });
        }

        public new void Add(Vector2Int key, BlueprintBlock value)
        {
            Size.Invalidate();

            base.Add(key, value);

            var anchoredBlueprintBlock = new AnchoredBlueprintBlock
            {
                gridAnchor = key,
                blueprintBlock = value
            };

            var context = anchoredBlueprintBlock.blueprintBlock.RuntimeContext;

            // Add mass to grid
            TypeUtils.OnAssignableFrom<IPhysicalBlockTemplateComponent>(context, (component) =>
            {
                Mass += component.PhysicalBlockTemplateComponent.Mass;
            });

            // Add peak consumption to grid
            TypeUtils.OnAssignableFrom<IPowerGeneratorBlockTemplateComponent>(context, (component) =>
            {
                PeakPowerGeneration += component.PowerGeneratorBlockTemplateComponent.PeakGeneration;
            });


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

        public new void Remove(Vector2Int key)
        {
            Size.Invalidate();

            var tilePositions = ShipEditorUtils.GetOccupiedTilesByAnchor(this[key], key);
            var anchoredBlueprintBlock = occupiedTiles[key];

            // Add mass to grid
            var context = anchoredBlueprintBlock.blueprintBlock.RuntimeContext;
            TypeUtils.OnAssignableFrom<IPhysicalBlockTemplateComponent>(context, (component) =>
            {
                Mass -= component.PhysicalBlockTemplateComponent.Mass;
            });

            // Remove neighbour references
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

        private IEnumerable<AnchoredBlueprintBlock> GetNeighbours(IEnumerable<Vector2Int> tilePositions)
        {
            // Get grid positions around block
            var bounds = new Generics.GridBounds(tilePositions);
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