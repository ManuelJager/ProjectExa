using Exa.Debugging;
using Exa.Math;
using Exa.Math.ControlSystems;
using System;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipNavigation : MonoBehaviour
    {
        public Rigidbody rb;
        private PidQuaternionController pidQuaternionController;
        private PdVector2Controller pdVector2Controller;
        private ThrusterFireAction thrusterFireAction;

        private float angleHint = 0f;

        public Ship ship;
        public ThrustVectors thrustVectors;

        [SerializeField] private bool continouslyApplySettings;

        [Header("PID-Quaternion-parameters")]
        [SerializeField] private float qProportionalBase;
        [SerializeField] private float qIntegral;
        [SerializeField] private float qDerivitive;

        [Header("PD-Position-parameters")]
        [SerializeField] private float pProportional;
        [SerializeField] private float pDerivitive;
        [SerializeField] private float maxVel;

        // NOTE: Replace this by a target interface
        private Vector2? lookAt = null;
        private Vector2? moveTo = null;

        private void Awake()
        {
            thrustVectors = new ThrustVectors();
            pidQuaternionController = new PidQuaternionController(qProportionalBase, qIntegral, qDerivitive);
            pdVector2Controller = new PdVector2Controller(pProportional, pDerivitive, 50f);
        }

        private void Start()
        {
            thrusterFireAction = new ThrusterFireAction(ship);
            ship.ActionScheduler.Add(thrusterFireAction);
        }

        /// <summary>
        /// Formely just a FixedUpdate, it gets called on the ship's FixedUpdate instead.
        /// </summary>
        public void ScheduledFixedUpdate()
        {
            if (continouslyApplySettings)
            {
                pidQuaternionController.Integral = qIntegral;
                pidQuaternionController.Derivitive = qDerivitive;

                pdVector2Controller.Proportional = pProportional;
                pdVector2Controller.Derivitive = pDerivitive;
                pdVector2Controller.MaxVel = maxVel;
            }

            UpdateHeading(ref angleHint);
            UpdateThrustVectors();
        }

        public void SetTurningMultiplier(float rate)
        {
            pidQuaternionController.Proportional = qProportionalBase * rate;
        }

        public void SetLookAt(Vector2? lookAt)
        {
            this.lookAt = lookAt;
        }

        public void SetMoveTo(Vector2? moveTo)
        {
            this.moveTo = moveTo;
        }

        private void UpdateThrustVectors()
        {
            // NOTE: This currently doesn't try to brake when no target is active
            if (moveTo == null)
            {
                thrusterFireAction.Update(Vector2.zero);
                return;
            }

            // Get the difference between the current position and the target position
            // Transform the difference to a local target vector for the pid controller
            var localTarget = moveTo.Value - (Vector2)transform.position;

            if (Systems.IsDebugging(DebugMode.Navigation))
            {
                Debug.DrawRay(transform.position, localTarget, Color.green);
            }

            var acceleration = pdVector2Controller.CalculateRequiredVelocity(
                transform.position,
                moveTo.Value,
                rb.velocity);

            // NOTE: Clamping the acceleration will usually result in drifting if the side thrusters aren't as powerful
            thrusterFireAction.Update(acceleration);
        }

        /// <summary>
        /// Rotate the ship towards a position in world space
        /// </summary>
        /// <param name="angleHint">The angle we are currently targetting, this angle is updated when there's a valid position to rotate to</param>
        private void UpdateHeading(ref float angleHint)
        {
            // NOTE: This currently doesn't try to stop rotation when no target is active
            if (lookAt == null)
            {
                AddTorqueTowards(angleHint);
                return;
            }

            var distance = lookAt.Value - (Vector2)transform.position;

            if (Systems.IsDebugging(DebugMode.Navigation))
            {
                Debug.DrawRay(transform.position, distance, Color.red);

                var headingDir = transform.right * ship.Blueprint.Blocks.MaxSize;
                Debug.DrawRay(transform.position, headingDir, Color.blue);
            }

            // Don't update the heading if target is very close. This is to prevent weird rotations
            if (distance.magnitude < 1f)
            {
                AddTorqueTowards(angleHint);
                return;
            }

            // Get the desired rotation
            angleHint = distance.GetAngle();

            AddTorqueTowards(angleHint);
        }

        private void AddTorqueTowards(float angle)
        {
            var desiredOrientation = Quaternion.Euler(0, 0, angle);

            // Calculate the angular acceleration 
            var angularAcceleration = pidQuaternionController.ComputeRequiredAngularAcceleration(
                transform.rotation,
                desiredOrientation,
                rb.angularVelocity,
                Time.fixedDeltaTime);

            rb.AddTorque(angularAcceleration, ForceMode.Acceleration);
        }
    }
}