using UnityEngine;

namespace Exa.Camera
{
    /// <summary>
    /// Represents a camera target the user may move
    /// </summary>
    public class UserTarget : CameraTarget
    {
        public Vector2 worldPosition;
        public Vector2 movementDelta;
        private float movementSpeedMultiplier = 1.5f;

        public UserTarget(CameraTargetSettings settings) 
            : base(settings) { }

        public void Tick() {
            worldPosition += movementDelta * GetMovementSpeed();
        }

        public override Vector2 GetWorldPosition() {
            var mouseOffset = Systems.Input.MouseOffsetFromCentre * GetCalculatedOrthoSize() * 0.1f;
            return worldPosition + mouseOffset;
        }

        public override float GetCalculatedOrthoSize() {
            return base.GetCalculatedOrthoSize() * 0.9f;
        }

        public void ImportValues(ICameraTarget otherTarget) {
            this.worldPosition = otherTarget.GetWorldPosition();
            this.ZoomScale = otherTarget.ZoomScale;
        }

        private float GetMovementSpeed() {
            return movementSpeedMultiplier * GetCalculatedOrthoSize() * Time.deltaTime;
        }
    }
}