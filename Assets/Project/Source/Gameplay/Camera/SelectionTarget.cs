using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Exa.Gameplay
{
    public class SelectionTarget : CameraTarget
    {
        private ShipSelection selection;

        public override bool TargetValid => selection.Any();

        public SelectionTarget(ShipSelection selection, CameraTargetSettings settings)
            : base(settings) {
            this.selection = selection;
            SetZoomFromOrtho(selection.Max(ship => ship.BlockGrid.MaxSize) * 0.75f);
        }

        public override Vector2 GetWorldPosition() {
            return selection.AveragePosition;
        }
    }
}