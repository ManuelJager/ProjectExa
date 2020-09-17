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
            Dampen(deltaTime);
        }

        private void Dampen(float deltaTime)
        {
            var velocityForce = GetLocalVelocityForce();

            // Get force for this frame
            var frameTargetForce = thrustVectors.GetForce(-velocityForce, DampeningThrustMultiplier) * deltaTime;
            ProcessTargetForce(ref frameTargetForce, -velocityForce / deltaTime);

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

        private void ProcessTargetForce(ref Vector2 targetForce, Vector2 velocityForce)
        {
            void ProcessAxis(ref float forceAxis, float velocityAxis)
            {
                if (Mathf.Abs(forceAxis) > Mathf.Abs(velocityAxis))
                {
                    forceAxis = velocityAxis;
                }
            }

            ProcessAxis(ref targetForce.x, velocityForce.x);
            ProcessAxis(ref targetForce.y, velocityForce.y);
        }

        private Vector2 GetLocalVelocityForce()
        {
            return GetLocalVelocity() * ship.rb.mass;
        }
    }
}