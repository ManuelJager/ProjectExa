using Exa.Debugging;
using Exa.Math;
using Exa.Math.ControlSystems;
using Exa.Ships.Targetting;
using System;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    [Obsolete]
    public class PidNavigation : INavigation
    {
        public Ship ship;

        private readonly ThrustVectors thrustVectors;
        private readonly PidQuaternionController pidQuaternionController;
        private readonly PdVector2Controller pdVector2Controller;
        private readonly PidThrusterFireAction pidThrusterFireAction;
        private readonly NavigationOptions options;
        private float angleHint = 0f;

        public ITarget LookAt { private get; set; }
        public ITarget MoveTo { private get; set; }
        public IThrustVectors ThrustVectors => thrustVectors;

        public PidNavigation(Ship ship, NavigationOptions options, float directionalThrust)
        {
            this.ship = ship;
            this.options = options;

            thrustVectors = new ThrustVectors(directionalThrust);
            pidQuaternionController = new PidQuaternionController(options.qProportionalBase, options.qIntegral, options. qDerivative);
            pdVector2Controller = new PdVector2Controller(options.pProportional, options.pDerivative, options.maxVel);
            pidThrusterFireAction = new PidThrusterFireAction(ship);

            ship.ActionScheduler.Add(pidThrusterFireAction);
        }

        /// <summary>
        /// Formerly just a FixedUpdate, it gets called on the ship's FixedUpdate instead.
        /// </summary>
        public void ScheduledFixedUpdate()
        {
            if (options.continuouslyApplySettings)
            {
                pidQuaternionController.Proportional = options.qProportionalBase * ship.state.GetTurningRate();
                pidQuaternionController.Integral = options.qIntegral;
                pidQuaternionController.Derivative = options.qDerivative;

                pdVector2Controller.Proportional = options.pProportional;
                pdVector2Controller.Derivitive = options.pDerivative;
                pdVector2Controller.MaxVel = options.maxVel;
            }

            UpdateHeading(ref angleHint);
            UpdateThrustVectors();
        }

        private void UpdateThrustVectors()
        {
            // NOTE: This currently doesn't try to brake when no target is active
            if (MoveTo == null)
            {
                pidThrusterFireAction.Update(Vector2.zero);
                return;
            }

            var currentPosition = (Vector2)options.transform.position;
            var targetPosition = MoveTo.GetPosition(currentPosition);

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
                ship.rb.velocity);

            // NOTE: Clamping the acceleration will usually result in drifting if the side thrusters aren't as powerful
            pidThrusterFireAction.Update(acceleration * ship.rb.mass);
        }

        /// <summary>
        /// Rotate the ship towards a position in world space
        /// </summary>
        /// <param name="angleHint">The angle we are currently targeting, this angle is updated when there's a valid position to rotate to</param>
        private void UpdateHeading(ref float angleHint) 
        {
            // NOTE: This currently doesn't try to stop rotation when no target is active
            if (LookAt == null)
            {
                AddTorqueTowards(angleHint);
                return;
            }

            var currentPosition = (Vector2)options.transform.position;
            var targetPosition = LookAt.GetPosition(currentPosition);
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

            var currentAngularVelocity = new Vector3(0, 0, ship.rb.angularVelocity * Mathf.Deg2Rad);

            // Calculate the angular acceleration
            var requiredAngularAcceleration = pidQuaternionController.ComputeRequiredAngularAcceleration(
                options.transform.rotation,
                desiredOrientation,
                currentAngularVelocity,
                Time.fixedDeltaTime).z;

            ship.rb.AddTorque(requiredAngularAcceleration * ship.rb.mass, ForceMode2D.Force);
        }
    }
}   