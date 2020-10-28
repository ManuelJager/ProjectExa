using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Exa.Gameplay
{
    public class SelectionTarget : ICameraTarget
    {
        private ShipSelection selection;

        public SelectionTarget(ShipSelection selection) {
            this.selection = selection;
        }

        public Vector2 GetWorldPosition() {
            return selection.AveragePosition;
        }

        public float GetOrthoSize() {
            return selection.Max(ship => ship.BlockGrid.MaxSize) * 2.5f;
        }

        public bool GetTargetValid() {
            return selection.Count > 0;
        }
    }
}