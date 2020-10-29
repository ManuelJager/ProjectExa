using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay
{
    public class ShipTarget : CameraTarget
    {
        private Ship ship;

        public override bool TargetValid => ship != null;

        public ShipTarget(Ship ship) {
            this.ship = ship;
        }

        public override Vector2 GetWorldPosition() {
            return ship.GetPosition();
        }

        public override float GetBaseOrthoSize() {
            return ship.BlockGrid.MaxSize * 2.5f;
        }
    }
}