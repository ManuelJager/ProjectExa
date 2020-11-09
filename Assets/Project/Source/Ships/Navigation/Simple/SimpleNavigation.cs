using Exa.Math;
using Exa.Ships.Targeting;
using Exa.Data;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class SimpleNavigation : INavigation
    {
        private readonly ThrustVectors thrustVectors;
        private readonly Ship ship;
        private readonly NavigationOptions options;

        public ITarget LookAt { private get; set; }
        public ITarget MoveTo { private get; set; }
        public IThrustVectors ThrustVectors => thrustVectors;

        public SimpleNavigation(Ship ship, NavigationOptions options, Scalar thrustModifier) {
            this.ship = ship;
            this.options = options;
            this.thrustVectors = new ThrustVectors(thrustModifier);
        }

        public void Update(float deltaTime) {
            if (LookAt != null) {
                UpdateRotation();
            }

            if (MoveTo != null) {
                UpdatePosition();
            }
        }

        private void UpdatePosition() {
            var currentPosition = ship.rb.position;
            var moveToTargetPosition = MoveTo.GetPosition(currentPosition);

            if (currentPosition == moveToTargetPosition) {
                ThrustVectors.SetGraphics(Vector2.zero);
                return;
            }

            var headingAngle = ship.rb.rotation;
            var direction = (moveToTargetPosition - currentPosition).Rotate(-headingAngle).normalized;
            ThrustVectors.SetGraphics(direction);

            var deltaTime = Time.fixedDeltaTime;
            var newPosition = Vector2.MoveTowards(currentPosition, moveToTargetPosition, 30 * deltaTime);

            ship.rb.position = newPosition;
        }

        private void UpdateRotation() {
            var currentPosition = ship.rb.position;
            var lookAtTargetPosition = LookAt.GetPosition(currentPosition);

            var lookAtDelta = lookAtTargetPosition - currentPosition;
            var currentRotation = new Vector3(0, 0, ship.rb.rotation);

            var targetRotation = new Vector3(0, 0, lookAtDelta.GetAngle());
            var deltaTime = Time.fixedDeltaTime;
            var newRotation = Vector3.RotateTowards(currentRotation, targetRotation, float.MaxValue, 100 * deltaTime);

            ship.rb.rotation = newRotation.z;
        }
    }
}