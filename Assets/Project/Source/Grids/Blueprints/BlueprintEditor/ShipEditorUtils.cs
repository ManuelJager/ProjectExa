using Exa.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public static class ShipEditorUtils
    {
        public static Vector3 GetRealPositionByAnchor(BlueprintBlock block, Vector2Int gridAnchor)
        {
            var size = block.RuntimeContext.Size - Vector2Int.one;

            var offset = new Vector2
            {
                x = size.x / 2f,
                y = size.y / 2f
            }.Rotate(block.Rotation);

            if (block.flippedX) offset.x = -offset.x;
            if (block.flippedY) offset.y = -offset.y;

            return new Vector3
            {
                x = offset.x + gridAnchor.x + 0.5f,
                y = offset.y + gridAnchor.y + 0.5f,
            };
        }

        public static IEnumerable<Vector2Int> GetOccupiedTilesByGhost(BlockGhost blockGhost)
        {
            var block = blockGhost.blueprintBlock;
            var gridAnchor = blockGhost.GridPos;
            return GetOccupiedTilesByAnchor(block, gridAnchor);
        }

        public static IEnumerable<Vector2Int> GetOccupiedTilesByAnchor(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            var block = anchoredBlueprintBlock.blueprintBlock;
            var gridAnchor = anchoredBlueprintBlock.gridAnchor;
            return GetOccupiedTilesByAnchor(block, gridAnchor);
        }

        public static IEnumerable<Vector2Int> GetOccupiedTilesByAnchor(BlueprintBlock block, Vector2Int gridAnchor)
        {
            var area = block.RuntimeContext.Size.Rotate(block.Rotation);

            if (block.flippedX) area.x = -area.x;
            if (block.flippedY) area.y = -area.y;

            return VectorHelpers.EnumerateVectors(area, gridAnchor);
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