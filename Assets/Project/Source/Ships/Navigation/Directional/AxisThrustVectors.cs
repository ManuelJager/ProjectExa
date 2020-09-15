using Exa.Data;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class AxisThrustVectors : IThrustVectors
    {
        private readonly ThrusterAxis horizontalThrusterAxis;
        private readonly ThrusterAxis verticalThrusterAxis;

        public AxisThrustVectors(Scalar thrustModifier)
        {
            horizontalThrusterAxis = new ThrusterAxis(thrustModifier);
            verticalThrusterAxis = new ThrusterAxis(thrustModifier);
        }

        public void Register(IThruster thruster)
        {
            SelectAxis(thruster, out var component).Register(thruster, component);
        }

        public void Unregister(IThruster thruster)
        {
            SelectAxis(thruster, out var component).Unregister(thruster, component);
        }

        public void Fire(Vector2 velocity)
        {
            horizontalThrusterAxis.Fire(velocity.x);
            verticalThrusterAxis.Fire(velocity.y);
        }

        public Vector2 Clamp(Vector2 forceDirection, float deltaTime)
        {
            return new Vector2
            {
                x = horizontalThrusterAxis.Clamp(forceDirection.x, deltaTime),
                y = verticalThrusterAxis.Clamp(forceDirection.y, deltaTime)
            };
        }

        public void SetGraphics(Vector2 directionScalar)
        {
            horizontalThrusterAxis.SetGraphics(directionScalar.x);
            verticalThrusterAxis.SetGraphics(directionScalar.y);
        }

        private ThrusterAxis SelectAxis(IThruster thruster, out bool positiveAxisComponent)
        {
            var block = thruster.Component.block;
            var direction = block.BlueprintBlock.Direction;

            positiveAxisComponent = direction <= 1;
            return direction % 2 == 0
                ? horizontalThrusterAxis
                : verticalThrusterAxis;
        }
    }
}