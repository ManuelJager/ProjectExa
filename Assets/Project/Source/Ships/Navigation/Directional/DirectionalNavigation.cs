using Exa.Data;
using Exa.Math;
using Exa.Ships.Targetting;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class DirectionalNavigation : INavigation
    {
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
            // TODO: Clamp the result of this to account for the current velocity, to prevent oscillation
            var targetForce = thrustVectors.Clamp(-velocityForce, deltaTime);

            // Transform force for this frame to velocity
            thrustVectors.Fire(targetForce / deltaTime);
            ship.rb.AddForce(targetForce);
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