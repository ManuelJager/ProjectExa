using UnityEngine;

namespace Exa.Gameplay
{
    /// <summary>
    /// Represents a camera target the user may move
    /// </summary>
    public class UserTarget : CameraTarget
    {
        public Vector2 worldPosition;
        public Vector2 movementDelta;
        private float movementSpeedMultiplier = 1.5f;
        private float baseOrthoSize = 30f;

        public UserTarget(CameraTargetSettings settings) 
            : base(settings) { }

        public void Tick() {
            worldPosition += movementDelta * GetMovementSpeed();
        }

        public override Vector2 GetWorldPosition() {
            return worldPosition;
        }

        public override float GetBaseOrthoSize() {
            return baseOrthoSize * 0.9f;
        }

        public void ImportValues(CameraTarget otherTarget) {
            this.worldPosition = otherTarget.GetWorldPosition();
            this.ZoomScale = otherTarget.ZoomScale;
            this.baseOrthoSize = otherTarget.GetBaseOrthoSize();
        }

        private float GetMovementSpeed() {
            return movementSpeedMultiplier * GetCalculatedOrthoSize() * Time.deltaTime;
        }
    }
}