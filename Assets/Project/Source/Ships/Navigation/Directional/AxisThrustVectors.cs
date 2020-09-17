using Exa.Data;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class AxisThrustVectors : IThrustVectors
    {
        private readonly ThrusterAxis horizontalThrusterAxis;
        private readonly ThrusterAxis verticalThrusterAxis;

        public ThrusterAxis XAxis
        {
            get => horizontalThrusterAxis;
        }

        public ThrusterAxis YAxis
        {
            get => verticalThrusterAxis;
        }

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

        public void Fire(Vector2 force)
        {
            horizontalThrusterAxis.Fire(force.x);
            verticalThrusterAxis.Fire(force.y);
        }

        public Vector2 GetForce(Vector2 forceDirection, Scalar thrustModifier)
        {
            return new Vector2
            {
                x = thrustModifier.GetValue(horizontalThrusterAxis.Clamp(forceDirection.x)),
                y = thrustModifier.GetValue(verticalThrusterAxis.Clamp(forceDirection.y))
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