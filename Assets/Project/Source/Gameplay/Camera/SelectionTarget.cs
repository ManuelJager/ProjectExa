using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Exa.Gameplay
{
    public class SelectionTarget : CameraTarget
    {
        private ShipSelection selection;

        public override bool TargetValid => selection.Any();

        public SelectionTarget(ShipSelection selection) {
            this.selection = selection;
        }

        public override Vector2 GetWorldPosition() {
            return selection.AveragePosition;
        }

        public override float GetBaseOrthoSize() {
            return selection.Max(ship => ship.BlockGrid.MaxSize) * 2.5f;
        }
    }
}