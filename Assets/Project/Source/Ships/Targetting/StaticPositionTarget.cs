using UnityEngine;

namespace Exa.Ships.Targetting
{
    public struct StaticPositionTarget : ITarget
    {
        private readonly Vector2 position;

        public StaticPositionTarget(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 GetPosition(Vector2 current)
        {
            return position;
        }
    }
}