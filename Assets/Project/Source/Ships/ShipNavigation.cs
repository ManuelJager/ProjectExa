using Exa.Debugging;
using Exa.Math.ControlSystems;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipNavigation : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        private PidQuaternionController pidQuaternionController;

        public Ship ship;

        [Header("PID-parameters")]
        [SerializeField] private float proportionalBase;
        [SerializeField] private float integral;
        [SerializeField] private float derivitive;

        // NOTE: Replace this by a target interface
        private Vector2? lookAt = null;

        private void Awake()
        {
            pidQuaternionController = new PidQuaternionController(proportionalBase, integral, derivitive);
        }

        private void FixedUpdate()
        {
            UpdateHeading();
        }

        public void SetTurningMultiplier(float rate)
        {
            pidQuaternionController.Proportional = proportionalBase * rate;
        }

        public void SetLookat(Vector2? lookAt)
        {
            this.lookAt = lookAt;
        }

        private void UpdateHeading()
        {
            if (lookAt == null) return; 

            var direction = lookAt.Value - (Vector2)transform.position;

            if (Systems.IsDebugging(DebugMode.Navigation))
            {
                Debug.DrawRay(transform.position, direction, Color.red);

                var headingDir = transform.right * ship.Blueprint.Blocks.MaxSize;
                Debug.DrawRay(transform.position, headingDir, Color.blue);
            }

            // Get the desired rotation
            var desiredAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var desiredOrientation = Quaternion.Euler(0, 0, desiredAngle);

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