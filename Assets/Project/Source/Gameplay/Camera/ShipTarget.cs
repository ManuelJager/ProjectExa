using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay
{
    public class ShipTarget : ICameraTarget
    {
        private Ship ship;

        public ShipTarget(Ship ship) {
            this.ship = ship;
        }

        public Vector2 GetWorldPosition() {
            return ship.GetPosition();
        }

        public float GetOrthoSize() {
            return ship.BlockGrid.MaxSize * 2.5f;
        }

        public bool GetTargetValid() {
            return ship != null;
        }
    }
}