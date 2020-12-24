using Exa.Data;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Math;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class AxisThrustVectors : MonoBehaviour, IThrustVectors
    {
        [SerializeField] private Vector2 currentDirection;
        [SerializeField] private Vector2 targetDirection;

        private ThrusterAxis xAxis;
        private ThrusterAxis yAxis;
        private Ship ship;

        public void Setup(Ship ship, Scalar thrustModifier) {
            this.ship = ship;
            xAxis = new ThrusterAxis(thrustModifier);
            yAxis = new ThrusterAxis(thrustModifier);
        }

        public void Update() {
            if (!ship.Active) return;
            return;
            MathUtils.MoveTowards(ref currentDirection, targetDirection, Time.deltaTime * 2f);

            xAxis.SetGraphics(currentDirection.x);
            yAxis.SetGraphics(currentDirection.y);
        }

        public void Register(IThruster thruster) {
            SelectAxis(thruster, out var component).Register(thruster, component);
        }

        public void Unregister(IThruster thruster) {
            SelectAxis(thruster, out var component).Unregister(thruster, component);
        }

        public void Fire(Vector2 force) {
            xAxis.Fire(force.x);
            yAxis.Fire(force.y);
        }

        public Vector2 GetForce(Vector2 forceDirection, Scalar thrustModifier) {
            var force = GetUnClampedForce(forceDirection);
            return thrustModifier.GetValue(force);
        }

        public Vector2 GetClampedForce(Vector2 forceDirection, Scalar thrustModifier) {
            var force = GetUnClampedForce(forceDirection);
            var clampedForce = MathUtils.GrowDirectionToMax(forceDirection.normalized, force);
            return thrustModifier.GetValue(clampedForce);
        }

        private Vector2 GetUnClampedForce(Vector2 forceDirection) {
            return new Vector2 {
                x = xAxis.Clamp(forceDirection.x),
                y = yAxis.Clamp(forceDirection.y)
            };
        }

        public void SetGraphics(Vector2 directionScalar) {
            targetDirection = directionScalar;
            xAxis.SetGraphics(targetDirection.x);
            yAxis.SetGraphics(targetDirection.y);
        }

        private ThrusterAxis SelectAxis(IThruster thruster, out bool positiveAxisComponent) {
            var block = thruster.Component.block;
            var direction = block.BlueprintBlock.Direction;

            positiveAxisComponent = direction <= 1;
            return direction % 2 == 0 ? xAxis : yAxis;
        }
    }
}