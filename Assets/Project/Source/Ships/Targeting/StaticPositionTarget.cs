using UnityEngine;

namespace Exa.Ships.Targetting
{
    public struct StaticPositionTarget : ITarget
    {
        private readonly Vector2 _position;

        public StaticPositionTarget(Vector2 position)
        {
            this._position = position;
        }

        public Vector2 GetPosition(Vector2 current)
        {
            return _position;
        }
    }
}