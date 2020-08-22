using System;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class DuplicateControllerException : Exception
    {
        public Vector2Int GridPos { get; set; }

        public DuplicateControllerException(Vector2Int gridPos)
            : base($"Cannot add controller at grid pos {gridPos} as this would cause a duplicate ")
        {
            GridPos = gridPos;
        }
    }
}