using Exa.Data;
using Exa.Debugging;
using Exa.Math;
using Exa.Ships.Targetting;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class DirectionalNavigation : INavigation
    {
        private static readonly Scalar DampeningThrustMultiplier = new Scalar(1.5f);
        private static readonly Scalar TargetThrustMultiplier = new Scalar(1f);

        private readonly Ship _ship;
        private readonly NavigationOptions _options;
        private readonly AxisThrustVectors _thrustVectors;

        public ITarget LookAt { private get; set; }
        public ITarget MoveTo { private get; set; }
        public IThrustVectors ThrustVectors => _thrustVectors;

        public DirectionalNavigation(Ship ship, NavigationOptions options, Scalar thrustModifier)
        {
            this._ship = ship;
            this._options = options;

            _thrustVectors = new AxisThrustVectors(thrustModifier);
        }

        public void Update(float deltaTime)
        {
            var velocityValues = GetLocalVelocity();

            // Calculate the force required to travel
            var frameTargetForce = Target(velocityValues, deltaTime);

            // Calculate the force required to dampen
            var frameDampenForce = Dampen(velocityValues, deltaTime);

            var resultFrameForce = MergeForces(frameTargetForce, frameDampenForce);

            if (DebugMode.Navigation.GetEnabled())
            {
                void DrawRay(Vector2 localFrameForce, Color color)
                {
                    var force = localFrameForce.Rotate(_ship.transform.rotation.eulerAngles.z) / deltaTime;
                    Debug.DrawRay(_ship.transform.position, force / _ship.rb.mass / 10, color);
                }

                DrawRay(frameTargetForce, Color.red);
                DrawRay(frameDampenForce, Color.blue);
                DrawRay(resultFrameForce, (Color.red + Color.blue) / 2);
            }

            Fire(resultFrameForce, deltaTime);
        }

        private Vector2 Target(VelocityValues velocityValues, float deltaTime)
        {
            if (MoveTo == null)
            {
                return Vector2.zero;
            }

            // Calculate the distance between the current and target position from the perspective of the ship
            var diff = GetLocalDifference(MoveTo);

            if (diff.magnitude < 1f)
            {
                return Vector2.zero;
            }

            Debug.Log(GetBrakeDistance(diff, velocityValues));

            return diff.magnitude > GetBrakeDistance(diff, velocityValues)
                ? _thrustVectors.GetClampedForce(diff, TargetThrustMultiplier) * deltaTime
                : Vector2.zero;
        }

        private Vector2 GetLocalDifference(ITarget target)
        {
            var currentPos = (Vector2) _ship.transform.position;
            var currentRotation = _ship.transform.rotation.eulerAngles.z;
            var diff = target.GetPosition(currentPos) - currentPos;
            return diff.Rotate(-currentRotation);
        }

        private float GetDecelerationVelocity(Vector2 direction, Scalar thrustModifier)
        {
            var force = _thrustVectors.GetClampedForce(direction, thrustModifier);
            return -(force / Mathf.Pow(_ship.rb.mass, 2)).magnitude;
        }

        // TODO: Fix this
        private float GetBrakeDistance(Vector2 diff, VelocityValues velocityValues)
        {
            var currentVelocity = velocityValues.localVelocity.magnitude;
            var deceleration = GetDecelerationVelocity(-diff, DampeningThrustMultiplier);
            return CalculateBrakeDistance(currentVelocity, 0, deceleration);
        }

        private float CalculateBrakeDistance(float currentVelocity, float targetVelocity, float deceleration)
        {
            var t = (targetVelocity - currentVelocity) / deceleration;
            return currentVelocity * t + deceleration * (t * t) / 2f;
        }

        private Vector2 Dampen(VelocityValues velocityValues, float deltaTime)
        {
            void ProcessAxis(ref float forceAxis, float velocityAxis)
            {
                if (Mathf.Abs(forceAxis) > Mathf.Abs(velocityAxis))
                {
                    forceAxis = velocityAxis;
                }
            }

            // Get force for this frame
            var frameTargetForce = _thrustVectors.GetForce(-velocityValues.localVelocityForce, DampeningThrustMultiplier) * deltaTime;
            var frameVelocityForce = -velocityValues.localVelocityForce / deltaTime;

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
            _thrustVectors.Fire(frameTargetForce / deltaTime);
            _ship.rb.AddForce(frameTargetForce, ForceMode2D.Force);
        }

        private VelocityValues GetLocalVelocity()
        {
            var zRotation = _ship.transform.rotation.eulerAngles.z;
            var localVelocity = _ship.rb.velocity.Rotate(-zRotation);
            return new VelocityValues
            {
                localVelocity = localVelocity,
                localVelocityForce = localVelocity * _ship.rb.mass
            };
        }

        private struct VelocityValues
        {
            public Vector2 localVelocity;
            public Vector2 localVelocityForce;
        }
    }
}