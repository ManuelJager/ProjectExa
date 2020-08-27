using Exa.Debugging;
using Exa.Math;
using Exa.Math.ControlSystems;
using Exa.Ships.Targetting;
using System;
using UnityEngine;

namespace Exa.Ships.Navigations
{
    public class ShipNavigation
    {
        public Ship ship;
        public ThrustVectors thrustVectors;

        private PidQuaternionController pidQuaternionController;
        private PdVector2Controller pdVector2Controller;
        private ThrusterFireAction thrusterFireAction;
        private NavigationOptions options;

        // NOTE: Replace this by a target interface
        private ITarget lookTarget = null;
        private ITarget moveTarget = null;
        private float angleHint = 0f;

        public ShipNavigation(Ship ship, NavigationOptions options, float directionalThrust)
        {
            this.ship = ship;
            this.options = options;

            thrustVectors = new ThrustVectors(directionalThrust);
            pidQuaternionController = new PidQuaternionController(options.qProportionalBase, options.qIntegral, options. qDerivitive);
            pdVector2Controller = new PdVector2Controller(options.pProportional, options.pDerivitive, options.maxVel);
            thrusterFireAction = new ThrusterFireAction(ship);

            ship.ActionScheduler.Add(thrusterFireAction);
        }

        /// <summary>
        /// Formely just a FixedUpdate, it gets called on the ship's FixedUpdate instead.
        /// </summary>
        public void ScheduledFixedUpdate()
        {
            if (options.continouslyApplySettings)
            {
                pidQuaternionController.Integral = options.qIntegral;
                pidQuaternionController.Derivitive = options.qDerivitive;

                pdVector2Controller.Proportional = options.pProportional;
                pdVector2Controller.Derivitive = options.pDerivitive;
                pdVector2Controller.MaxVel = options.maxVel;
            }

            UpdateHeading(ref angleHint);
            UpdateThrustVectors();
        }

        public void SetTurningMultiplier(float rate)
        {
            pidQuaternionController.Proportional = options.qProportionalBase * rate;
        }

        public void SetLookAt(ITarget lookTarget)
        {
            this.lookTarget = lookTarget;
        }

        public void SetMoveTo(ITarget moveTarget)
        {
            this.moveTarget = moveTarget;
        }

        private void UpdateThrustVectors()
        {
            // NOTE: This currently doesn't try to brake when no target is active
            if (moveTarget == null)
            {
                thrusterFireAction.Update(Vector2.zero);
                return;
            }

            var currentPosition = (Vector2)options.transform.position;
            var targetPosition = moveTarget.GetPosition(currentPosition);

            // Get the difference between the current position and the target position
            // Transform the difference to a local target vector for the pid controller
            var distance = targetPosition - currentPosition;

            if (Systems.IsDebugging(DebugMode.Navigation))
            {
                Debug.DrawRay(options.transform.position, distance, Color.green);
            }

            var acceleration = pdVector2Controller.CalculateRequiredVelocity(
                options.transform.position,
                targetPosition,
                ship.rigidbody.velocity);

            // NOTE: Clamping the acceleration will usually result in drifting if the side thrusters aren't as powerful
            thrusterFireAction.Update(acceleration * ship.rigidbody.mass);
        }

        /// <summary>
        /// Rotate the ship towards a position in world space
        /// </summary>
        /// <param name="angleHint">The angle we are currently targetting, this angle is updated when there's a valid position to rotate to</param>
        private void UpdateHeading(ref float angleHint) 
        {
            // NOTE: This currently doesn't try to stop rotation when no target is active
            if (lookTarget == null)
            {
                AddTorqueTowards(angleHint);
                return;
            }

            var currentPosition = (Vector2)options.transform.position;
            var targetPosition = lookTarget.GetPosition(currentPosition);
            var distance = targetPosition - currentPosition;

            if (Systems.IsDebugging(DebugMode.Navigation))
            {
                Debug.DrawRay(options.transform.position, distance, Color.red);

                var headingDir = options.transform.right * ship.Blueprint.Blocks.MaxSize;
                Debug.DrawRay(options.transform.position, headingDir, Color.blue);
            }

            //// Don't update the heading if target is very close. This is to prevent weird rotations
            //if (distance.magnitude < 1f)
            //{
            //    AddTorqueTowards(angleHint);
            //    return;
            //}

            // Get the desired rotation
            angleHint = distance.GetAngle();

            AddTorqueTowards(angleHint);
        }

        private void AddTorqueTowards(float angle)
        {
            var desiredOrientation = Quaternion.Euler(0, 0, angle);

            // Calculate the angular acceleration 
            var angularAcceleration = pidQuaternionController.ComputeRequiredAngularAcceleration(
                options.transform.rotation,
                desiredOrientation,
                ship.rigidbody.angularVelocity,
                Time.fixedDeltaTime);

            ship.rigidbody.AddTorque(angularAcceleration, ForceMode.Acceleration);
        }
    }
}