using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exa.Types.Binding;
using Exa.Types.Generics;
using UnityEngine;
using Exa.Utils;

namespace Exa.Grids
{
    public class Grid<T> : IEnumerable<T>
        where T : class, IGridMember
    {
        public LazyCache<Vector2Int> Size { get; protected set; }

        // NOTE: Grid totals are affected by the context of the blueprint, since they will be subject to change because of tech
        // TODO: Replace the reference by a manager that handles totals versioning
        public virtual GridTotals Totals { get; }
        protected ObservableCollection<T> GridMembers { get; set; }
        protected Dictionary<Vector2Int, T> OccupiedTiles { get; set; }
        protected Dictionary<T, List<T>> NeighbourDict { get; set; }

        public T Controller { get; protected set; }

        public float MaxSize {
            get {
                Vector2Int size = Size;
                return Mathf.Max(size.x, size.y);
            }
        }

        public Grid(
            LazyCache<Vector2Int> size = null,
            GridTotals totals = null,
            ObservableCollection<T> gridMembers = null,
            Dictionary<Vector2Int, T> occupiedTiles = null,
            Dictionary<T, List<T>> neighbourDict = null) {
            Size = size ?? new LazyCache<Vector2Int>(() => {
                var bounds = new GridBounds(OccupiedTiles.Keys);
                return bounds.GetDelta();
            });

            Totals = totals ?? new GridTotals();
            GridMembers = gridMembers ?? new ObservableCollection<T>();
            OccupiedTiles = occupiedTiles ?? new Dictionary<Vector2Int, T>();
            NeighbourDict = neighbourDict ?? new Dictionary<T, List<T>>();
        }

        public virtual void Add(T gridMember) {
            Size.Invalidate();

            // Assign controller reference
            if (gridMember.GetIsController() && Controller == null) {
                Controller = gridMember;
            }

            GridMembers.Add(gridMember);
            gridMember.AddGridTotals(Totals);

            // Get grid positions of blueprint block
            var tilePositions = gridMember.GetTileClaims();

            EnsureNeighbourKeyIsCreated(gridMember);

            // Add neighbour references
            foreach (var neighbour in this.GetNeighbours(tilePositions)) {
                NeighbourDict[neighbour].Add(gridMember);
                NeighbourDict[gridMember].Add(neighbour);
            }

            foreach (var tilePosition in tilePositions) {
                OccupiedTiles.Add(tilePosition, gridMember);
            }
        }

        public virtual T Remove(Vector2Int key) {
            Size.Invalidate();
            var gridMember = GetMember(key);
            Remove(gridMember);
            return gridMember;
        }

        public virtual void Remove(T gridMember) {
            // Remove controller reference
            if (ReferenceEquals(gridMember, Controller)) {
                Controller = default;
            }

            var tilePositions = gridMember.GetTileClaims();

            GridMembers.Remove(gridMember);
            gridMember.RemoveGridTotals(Totals);

            // Remove neighbour references
            foreach (var neighbour in NeighbourDict[gridMember]) {
                NeighbourDict[neighbour].Remove(gridMember);
            }

            NeighbourDict.Remove(gridMember);

            foreach (var occupiedTile in tilePositions) {
                OccupiedTiles.Remove(occupiedTile);
            }
        }

        public bool ContainsMember(Vector2Int gridPos) {
            return OccupiedTiles.ContainsKey(gridPos);
        }

        public bool TryGetMember(Vector2Int gridPos, out T member) {
            var containsMember = ContainsMember(gridPos);
            member = containsMember ? GetMember(gridPos) : null;
            return containsMember;
        }

        public T GetMember(Vector2Int gridPos) {
            return OccupiedTiles[gridPos];
        }

        public BlueprintBlock GetBlock(Vector2Int gridPos) {
            return OccupiedTiles[gridPos].BlueprintBlock;
        }

        public Vector2Int GetAnchor(Vector2Int gridPos) {
            return OccupiedTiles[gridPos].GridAnchor;
        }

        public bool HasOverlap(IEnumerable<Vector2Int> gridPositions) {
            return OccupiedTiles
                .Select(item => item.Key)
                .Intersect(gridPositions)
                .Any();
        }

        public int GetMemberCount() {
            return GridMembers.Count;
        }

        public int GetOccupiedTileCount() {
            return OccupiedTiles.Count;
        }

        protected void EnsureNeighbourKeyIsCreated(T gridMember) {
            if (!NeighbourDict.ContainsKey(gridMember)) {
                NeighbourDict[gridMember] = new List<T>();
            }
        }

        public int GetNeighbourCount(T gridMember) {
            return NeighbourDict[gridMember].Count;
        }

        public IEnumerator<T> GetEnumerator() {
            return GridMembers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GridMembers.GetEnumerator();
        }
    }
}