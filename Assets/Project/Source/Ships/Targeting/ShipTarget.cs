using UnityEngine;

namespace Exa.Ships.Targeting
{
    public readonly struct ShipTarget : IWeaponTarget
    {
        private readonly Ship ship;

        public ShipTarget(Ship ship) {
            this.ship = ship;
        }

        public Vector2 GetPosition(Vector2 current) {
            return ship.Controller.transform.position;
        }

        public bool GetTargetValid() {
            return ship != null;
        }
    }
}