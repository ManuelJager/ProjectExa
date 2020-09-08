using Exa.Math;
using Exa.Ships.Targetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class SimpleNavigation : INavigation
    {
        private readonly ThrustVectors thrustVectors;
        private readonly Ship ship;
        private readonly NavigationOptions options;

        private ITarget moveToTarget;
        private ITarget lookAtTarget;

        public IThrustVectors ThrustVectors => thrustVectors;

        public SimpleNavigation(Ship ship, NavigationOptions options, float directionalThrust)
        {
            this.ship = ship;
            this.options = options;
            this.thrustVectors = new ThrustVectors(directionalThrust);
        }

        public void ScheduledFixedUpdate()
        {
            if (lookAtTarget != null)
            {
                UpdateRotation();
            }

            if (moveToTarget != null)
            {
                UpdatePosition();
            }
        }

        public void SetLookAt(ITarget target)
        {
            lookAtTarget = target;
        }

        public void SetMoveTo(ITarget target)
        {
            moveToTarget = target;
        }

        private void UpdatePosition()
        {
            var currentPosition = (Vector2)ship.transform.position;
            var moveToTargetPosition = moveToTarget.GetPosition(currentPosition);

            if (currentPosition == moveToTargetPosition)
            {
                ThrustVectors.SetGraphics(Vector2.zero);
                return;
            }

            var headingAngle = ship.transform.rotation.eulerAngles.z;
            var direction = (moveToTargetPosition - currentPosition).Rotate(-headingAngle).normalized;
            ThrustVectors.SetGraphics(direction);

            var deltaTime = Time.fixedDeltaTime;
            var newPosition = Vector2.MoveTowards(currentPosition, moveToTargetPosition, 30 * deltaTime);

            ship.transform.position = newPosition;
        }

        private void UpdateRotation()
        {
            var currentPosition = (Vector2)ship.transform.position;
            var lookAtTargetPosition = lookAtTarget.GetPosition(currentPosition);

            var lookAtDelta = lookAtTargetPosition - currentPosition;
            var currentRotation = ship.transform.rotation.eulerAngles;

            var targetRotation = new Vector3(0, 0, lookAtDelta.GetAngle());
            var deltaTime = Time.fixedDeltaTime;
            var newRotation = Vector3.RotateTowards(currentRotation, targetRotation, float.MaxValue, 100 * deltaTime);

            ship.transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
