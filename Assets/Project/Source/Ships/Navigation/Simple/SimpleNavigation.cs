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
            var targetRotation = lookAtDelta.GetAngle();
            var currentRotation = ship.rb.rotation;

            ship.rb.rotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, 100 * Time.fixedDeltaTime);
        }
    }
}