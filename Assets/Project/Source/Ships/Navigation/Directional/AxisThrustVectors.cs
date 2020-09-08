using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class AxisThrustVectors : IThrustVectors
    {
        private readonly ThrusterAxis horizontalThrusterAxis;
        private readonly ThrusterAxis verticalThrusterAxis;

        public AxisThrustVectors(float directionalThrust)
        {
            horizontalThrusterAxis = new ThrusterAxis(directionalThrust);
            verticalThrusterAxis = new ThrusterAxis(directionalThrust);
        }

        public void Register(IThruster thruster)
        {
            SelectAxis(thruster, out var component).Register(thruster, component);
        }

        public void Unregister(IThruster thruster)
        {
            SelectAxis(thruster, out var component).Unregister(thruster, component);
        }

        public void SetGraphics(Vector2 directionScalar)
        {
            horizontalThrusterAxis.Fire(directionScalar.x);
            verticalThrusterAxis.Fire(directionScalar.y);
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