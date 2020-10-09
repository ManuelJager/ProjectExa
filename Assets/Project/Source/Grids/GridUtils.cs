using Exa.Math;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids
{
    public static class GridUtils
    {
        public static IEnumerable<T> GetNeighbours<T>(this Grid<T> grid, IEnumerable<Vector2Int> tilePositions)
            where T : IGridMember
        {
            // Get grid positions around block
            var bounds = new GridBounds(tilePositions);
            var neighbourPositions = bounds.GetAdjacentPositions();

            var neighbours = new List<T>();

            foreach (var neighbourPosition in neighbourPositions)
            {
                if (grid.ContainsMember(neighbourPosition))
                {
                    var neighbour = grid.GetMember(neighbourPosition);
                    if (!neighbours.Contains(neighbour))
                    {
                        neighbours.Add(neighbour);
                        yield return neighbour;
                    }
                }
            }
        }

        public static IEnumerable<Vector2Int> GetOccupiedTilesByAnchor(IGridMember gridMember)
        {
            var block = gridMember.BlueprintBlock;
            var gridAnchor = gridMember.GridAnchor;
            var area = block.Template.size.Rotate(block.Rotation);

            if (block.flippedX) area.x = -area.x;
            if (block.flippedY) area.y = -area.y;

            return MathUtils.EnumerateVectors(area, gridAnchor);
        }

        public static Vector2Int GetMirroredGridPos(Vector2Int size, Vector2Int gridPos)
        {
            return new Vector2Int
            {
                x = gridPos.x,
                y = size.y - 1 - gridPos.y
            };
        }

        public static void ConditionallyApplyToMirror(Vector2Int? gridPos, Vector2Int size, Action<Vector2Int> action)
        {
            if (gridPos == null) return;

            var realGridPos = gridPos.GetValueOrDefault();
            var mirroredGridPos = GetMirroredGridPos(size, realGridPos);
            if (realGridPos == mirroredGridPos) return;
            action(mirroredGridPos);
        }
    }
}