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
            var velocityForce = GetLocalVelocityForce();

            // Calculate the force required to travel
            var frameTargetForce = Target(Vector2.zero);

            // Calculate the force required to dampen
            var frameDampenForce = Dampen(velocityForce, deltaTime);

            var resultFrameForce = MergeForces(frameTargetForce, frameDampenForce);
            Fire(resultFrameForce, deltaTime);
        }

        // TODO: Implement
        private Vector2 Target(Vector2 target)
        {
            return Vector2.zero;
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
            float MergeComponent(float frameTargetForceComponent, float frameDampenForceComponent)
            {
                return frameTargetForceComponent == 0f
                    ? frameDampenForceComponent
                    : 0f;
            }

            return new Vector2
            {
                x = MergeComponent(frameTargetForce.x, frameDampenForce.x),
                y = MergeComponent(frameTargetForce.y, frameDampenForce.y)
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

        private Vector2 GetLocalVelocityForce()
        {
            return GetLocalVelocity() * ship.rb.mass;
        }
    }
}