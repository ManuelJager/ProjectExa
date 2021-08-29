using Exa.Debugging;
using NaughtyAttributes;
using UnityEngine;

namespace Exa.Ships.Rotation {
    public class RotationController : MonoBehaviour {
        private float heading;
        private float bearing;
        private float angularAcceleration;
        private float turnSpeed;

        [SerializeField, ReadOnly] private float maxTorque = 7.5f;
        [SerializeField, ReadOnly] private float stopThreshold = 0.3f;
        [SerializeField, ReadOnly] Vector2 targetVector;
        [SerializeField] private Rigidbody2DProxy proxy;

        private void Awake() {
            SetMaxTorque(maxTorque);
            SetTargetVector(Vector2.zero);
        }

        private void FixedUpdate() {
            turnSpeed = proxy.AngularVelocity;

            if (targetVector != Vector2.zero) {
                targetVector += proxy.Velocity * Time.fixedDeltaTime;

                if (S.Instance != null && DebugMode.Navigation.IsEnabled()) {
                    Debug.DrawLine(transform.position, targetVector);
                }

                // Cache transform
                var self = transform;

                // Calculate the angle from the current rotation to the target vector
                var rot = Quaternion.FromToRotation(
                        fromDirection: self.right * -1f,
                        toDirection: targetVector - (Vector2) self.position
                    )
                    .eulerAngles.z;

                var difference = 360f - rot - 180f;

                // Calculate the distance to stop given the current turn speed and angular acceleration
                var stopDistance = angularAcceleration * Mathf.Pow(turnSpeed / angularAcceleration, 2f) / 2f;
                var torqueDirection = -1f * Mathf.Sign(difference);

                // If the stop distance is longer than the difference between the current and target angle, reverse the direction of the torque
                if (stopDistance >= Mathf.Abs(difference)) {
                    torqueDirection *= -1f;
                }

                // If the distance is longer than the threshold, apply the torque, otherwise zero the angular velocity
                if (Mathf.Abs(difference) > stopThreshold) {
                    proxy.AddTorque(torqueDirection * maxTorque);
                } else {
                    proxy.AngularVelocity = 0;
                    targetVector = Vector3.zero;
                }
            }
        }

        public void SetMaxTorque(float maxTorque) {
            this.maxTorque = maxTorque;
            angularAcceleration = maxTorque / proxy.InertiaTensor * Mathf.Rad2Deg;
        }

        public void SetTargetVector(Vector3 newTarget) {
            targetVector = newTarget;
        }
    }
}