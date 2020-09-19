using Exa.Data;
using Exa.Debugging;
using Exa.Math;
using Exa.Ships.Targetting;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class DirectionalNavigation : INavigation
    {
        private static readonly Scalar DampeningThrustMultiplier = new Scalar(1);
        private static readonly Scalar TargetThrustMultiplier = new Scalar(1);

        private readonly Ship ship;
        private readonly NavigationOptions options;
        private readonly AxisThrustVectors thrustVectors;

        public ITarget LookAt { private get; set; }
        public ITarget MoveTo { private get; set; }
        public IThrustVectors ThrustVectors => thrustVectors;

        public DirectionalNavigation(Ship ship, NavigationOptions options, Scalar thrustModifier)
        {
            this.ship = ship;
            this.options = options;

            thrustVectors = new AxisThrustVectors(thrustModifier);
        }

        public void Update(float deltaTime)
        {
            var velocity = GetLocalVelocity();
            // Noted as a vector of kN
            var velocityForce = velocity * ship.rb.mass;

            // Calculate the force required to travel
            var frameTargetForce = Target(velocity, ship.transform.position);

            // Calculate the force required to dampen
            var frameDampenForce = Dampen(velocityForce, deltaTime);

            var resultFrameForce = MergeForces(frameTargetForce, frameDampenForce);
            Fire(resultFrameForce, deltaTime);
        }

        private Vector2 Target(Vector2 currentVelocity, Vector2 currentPosition)
        {
            return Vector2.zero;

            if (MoveTo == null)
            {
                return Vector2.zero;
            }

            //var targetPosition = MoveTo.GetPosition(currentPosition);

            //// Calculate a heading a distance to the target
            //var headingToTarget = targetPosition - currentPosition;
            //var distanceToTarget = headingToTarget.magnitude;

            //// Calculate a force, and a deceleration vector in the opposite direction of the heading
            //var decelerationForce = thrustVectors.GetForce(currentPosition - targetPosition, DampeningThrustMultiplier);
            //var deceleration = decelerationForce / ship.rb.mass;

            //// Calculate the distance to brake to the target
            //var brakeDistance = CalculateBrakeDistance(currentVelocity.magnitude, deceleration.magnitude);

            //return distanceToTarget > brakeDistance 
            //    ? thrustVectors.GetForce(headingToTarget, TargetThrustMultiplier)
            //    : Vector2.zero;
        }

        private float CalculateBrakeDistance(float currentVelocity, float targetVelocity, float deceleration)
        {
            var t = (targetVelocity - currentVelocity) / deceleration;
            return currentVelocity * t + deceleration * (t * t) / 2f;
        }

        private Vector2 Dampen(Vector2 velocityForce, float deltaTime)
        {
            void ProcessAxis(ref float forceAxis, float velocityAxis)
            {
                if (Mathf.Abs(forceAxis) > Mathf.Abs(velocityAxis))
                {
                    forceAxis = velocityAxis;
                }
            }

            // Get force for this frame
            var frameTargetForce = thrustVectors.GetForce(-velocityForce, DampeningThrustMultiplier) * deltaTime;
            var frameVelocityForce = -velocityForce / deltaTime;

            ProcessAxis(ref frameTargetForce.x, frameVelocityForce.x);
            ProcessAxis(ref frameTargetForce.y, frameVelocityForce.y);

            return frameTargetForce;
        }

        private Vector2 MergeForces(Vector2 frameTargetForce, Vector2 frameDampenForce)
        {
            // TODO: Make this branch-less
            float ProcessAxis(float targetComponent, float dampenComponent)
            {
                if (targetComponent == 0f) return dampenComponent;

                // Check if the target and the dampen component have different signs
                return targetComponent > 0f ^ dampenComponent > 0f
                    // If so, return the target vector component
                    ? targetComponent
                    // Otherwise, return biggest of the two components
                    : Mathf.Abs(targetComponent) > Mathf.Abs(dampenComponent)
                        ? targetComponent
                        : dampenComponent;
            }

            return new Vector2
            {
                x = ProcessAxis(frameTargetForce.x, frameDampenForce.x),
                y = ProcessAxis(frameTargetForce.y, frameDampenForce.y)
            };
        }

        private void Fire(Vector2 frameTargetForce, float deltaTime)
        {
            // Transform force for this frame to velocity
            thrustVectors.Fire(frameTargetForce / deltaTime);
            ship.rb.AddForce(frameTargetForce);
        }

        private Vector2 GetLocalVelocity()
        {
            var zRotation = ship.transform.rotation.eulerAngles.z;
            var localVelocity = ship.rb.velocity.Rotate(-zRotation);
            return localVelocity;
        }
    }
}