using Exa.Debugging;
using Exa.Utils;
using UnityEngine;

namespace Exa.Ships.Rotation {
    public class RotationController : MonoBehaviour {
        private float heading;
        private float bearing;
        private float angularAcceleration;
        private float turnSpeed;

        [SerializeField] private float maxTorque = 1f;
        [SerializeField] private float stopThresholdMultiplier = 0.001f;
        [SerializeField] private float baseStopThreshold = 0.1f;
        [SerializeField] private Rigidbody2D rb;
        Vector2? targetVector;

        private void Awake() {
            SetMaxTorque(maxTorque);
        }

        private void FixedUpdate() {
            turnSpeed = rb.angularVelocity;
            angularAcceleration = maxTorque / rb.inertia * Mathf.Rad2Deg;
            
            if (targetVector.GetHasValue(out var value)) {
                targetVector += rb.velocity * Time.fixedDeltaTime;

                if (S.Instance == null || DebugMode.Navigation.IsEnabled()) {
                    Debug.DrawLine(rb.worldCenterOfMass, value);
                }

                var localSpaceTargetVector = value - rb.worldCenterOfMass;
                
                // Calculate the angle from the current rotation to the target vector
                var rot = Quaternion.FromToRotation(transform.right * -1f, localSpaceTargetVector);

                var difference = 360f - rot.eulerAngles.z - 180f;

                // Calculate the distance to stop given the current turn speed and angular acceleration
                var stopDistance = angularAcceleration * Mathf.Pow(turnSpeed / angularAcceleration, 2f) / 2f;
                var torqueDirection = -1f * Mathf.Sign(difference);

                // If the stop distance is longer than the difference between the current and target angle, reverse the direction of the torque
                if (stopDistance >= Mathf.Abs(difference)) {
                    torqueDirection *= -1f;
                }

                // If the distance is longer than the threshold, apply the torque, otherwise zero the angular velocity
                if (Mathf.Abs(difference) > angularAcceleration * stopThresholdMultiplier + baseStopThreshold) {
                    rb.AddTorque(torqueDirection * maxTorque);
                } else {
                    rb.angularVelocity = 0;
                    targetVector = null;
                }
            }
        }

        public void SetMaxTorque(float maxTorque) {
            this.maxTorque = maxTorque;
        }

        public void SetTargetVector(Vector3 newTarget) {
            targetVector = newTarget;
        }
    }
}