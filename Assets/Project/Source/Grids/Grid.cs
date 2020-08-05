using Exa.Generics;
using Exa.Grids.Blueprints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exa.Grids
{
    public class Grid<T>
        where T : IGridMember
    {
        public LazyCache<Vector2Int> Size { get; protected set; }
        public LazyCache<Vector2> CentreOfMass { get; protected set; }
        public List<T> GridMembers { get; protected set; }
        protected Dictionary<Vector2Int, T> OccupiedTiles { get; set; }
        protected Dictionary<T, List<T>> NeighbourDict { get; set; }

        public Grid()
        {
            Size = new LazyCache<Vector2Int>(() =>
            {
                var bounds = new GridBounds(OccupiedTiles.Keys);
                return bounds.GetDelta();
            });

            CentreOfMass = new LazyCache<Vector2>(this.CalculateCentreOfMass);
            GridMembers = new List<T>();
            OccupiedTiles = new Dictionary<Vector2Int, T>();
            NeighbourDict = new Dictionary<T, List<T>>();
        }

        public virtual void Add(T gridMember)
        {
            Size.Invalidate();
            CentreOfMass.Invalidate();

            GridMembers.Add(gridMember);

            // Get grid positions of blueprint block
            var tilePositions = GridUtils.GetOccupiedTilesByAnchor(gridMember);

            EnsureNeighbourKeyIsCreated(gridMember);

            // Add neighbour references
            foreach (var neighbour in this.GetNeighbours(tilePositions))
            {
                NeighbourDict[neighbour].Add(gridMember);
                NeighbourDict[gridMember].Add(neighbour);
            }

            foreach (var tilePosition in tilePositions)
            {
                OccupiedTiles.Add(tilePosition, gridMember);
            }
        }

        public virtual T Remove(Vector2Int key)
        {
            Size.Invalidate();
            CentreOfMass.Invalidate();

            var gridMember = GetMember(key);
            var tilePositions = GridUtils.GetOccupiedTilesByAnchor(gridMember);

            GridMembers.Remove(gridMember);

            // Remove neighbour references
            foreach (var neighbour in NeighbourDict[gridMember])
            {
                NeighbourDict[neighbour].Remove(gridMember);
            }

            NeighbourDict.Remove(gridMember);

            foreach (var occupiedTile in tilePositions)
            {
                OccupiedTiles.Remove(occupiedTile);
            }

            return gridMember;
        }

        public bool ContainsMember(Vector2Int gridPos)
        {
            return OccupiedTiles.ContainsKey(gridPos);
        }

        public T GetMember(Vector2Int gridPos)
        {
            return OccupiedTiles[gridPos];
        }

        public BlueprintBlock GetBlock(Vector2Int gridPos)
        {
            return OccupiedTiles[gridPos].BlueprintBlock;
        }

        public Vector2Int GetAnchor(Vector2Int gridPos)
        {
            return OccupiedTiles[gridPos].GridAnchor;
        }

        public bool HasOverlap(IEnumerable<Vector2Int> gridPositions)
        {
            return OccupiedTiles
                .Select((item) => item.Key)
                .Intersect(gridPositions)
                .Any();
        }

        public int GetMemberCount()
        {
            return GridMembers.Count;
        }

        public int GetOccupiedTileCount()
        {
            return OccupiedTiles.Count;
        }

        protected void EnsureNeighbourKeyIsCreated(T gridMember)
        {
            if (!NeighbourDict.ContainsKey(gridMember))
            {
                NeighbourDict[gridMember] = new List<T>();
            }
        }
    }
}
