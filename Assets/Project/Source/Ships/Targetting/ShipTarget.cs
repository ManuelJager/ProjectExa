using UnityEngine;

namespace Exa.Ships.Targetting
{
    public struct ShipTarget : ITarget
    {
        private Ship ship;

        public ShipTarget(Ship ship)
        {
            this.ship = ship;
        }

        public Vector2 GetPosition(Vector2 current)
        {
            return ship.Controller.transform.position;
        }
    }
}