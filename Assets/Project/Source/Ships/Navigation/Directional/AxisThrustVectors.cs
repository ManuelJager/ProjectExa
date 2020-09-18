using Exa.Data;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class AxisThrustVectors : IThrustVectors
    {
        public ThrusterAxis XAxis { get; }
        public ThrusterAxis YAxis { get; }

        public AxisThrustVectors(Scalar thrustModifier)
        {
            XAxis = new ThrusterAxis(thrustModifier);
            YAxis = new ThrusterAxis(thrustModifier);
        }

        public void Register(IThruster thruster)
        {
            SelectAxis(thruster, out var component).Register(thruster, component);
        }

        public void Unregister(IThruster thruster)
        {
            SelectAxis(thruster, out var component).Unregister(thruster, component);
        }

        public void Fire(Vector2 force)
        {
            XAxis.Fire(force.x);
            YAxis.Fire(force.y);
        }

        public Vector2 GetForce(Vector2 forceDirection, Scalar thrustModifier)
        {
            return new Vector2
            {
                x = thrustModifier.GetValue(XAxis.Clamp(forceDirection.x)),
                y = thrustModifier.GetValue(YAxis.Clamp(forceDirection.y))
            };
        }

        public void SetGraphics(Vector2 directionScalar)
        {
            XAxis.SetGraphics(directionScalar.x);
            YAxis.SetGraphics(directionScalar.y);
        }

        private ThrusterAxis SelectAxis(IThruster thruster, out bool positiveAxisComponent)
        {
            var block = thruster.Component.block;
            var direction = block.BlueprintBlock.Direction;

            positiveAxisComponent = direction <= 1;
            return direction % 2 == 0
                ? XAxis
                : YAxis;
        }
    }
}