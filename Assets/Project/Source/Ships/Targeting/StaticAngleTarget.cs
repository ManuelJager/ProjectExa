using Exa.Math;
using UnityEngine;

namespace Exa.Ships.Targetting
{
    public struct StaticAngleTarget : ITarget
    {
        private readonly float _directionAngle;
        private readonly float _magnitude;

        public StaticAngleTarget(float directionAngle, float magnitude = 10)
        {
            this._directionAngle = directionAngle;
            this._magnitude = magnitude;
        }

        public StaticAngleTarget(Vector2 from, Vector2 to, float magnitude = 10)
        {
            this._directionAngle = (to - from).GetAngle();
            this._magnitude = magnitude;
        }

        public Vector2 GetPosition(Vector2 current)
        {
            var offset = (Vector2.right * _magnitude).Rotate(_directionAngle);
            var directionVector = current + offset;
            return directionVector;
        }
    }
}