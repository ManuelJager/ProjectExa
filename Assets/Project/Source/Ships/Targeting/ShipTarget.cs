using UnityEngine;

namespace Exa.Ships.Targetting
{
    public struct ShipTarget : ITarget
    {
        private readonly Ship _ship;

        public ShipTarget(Ship ship)
        {
            this._ship = ship;
        }

        public Vector2 GetPosition(Vector2 current)
        {
            return _ship.Controller.transform.position;
        }
    }
}