using UnityEngine;

namespace Exa.Gameplay
{
    /// <summary>
    /// Represents the centre of 
    /// </summary>
    public class UserTarget : ICameraTarget
    {
        public Vector2 worldPosition;
        public Vector2 movementDelta;
        private float movementSpeed = 30f;
        private float orthoSize = 30f;

        public float GetOrthoSize() {
            return orthoSize * 0.9f;
        }

        public bool GetTargetValid() {
            return true;
        }

        public void Tick() {
            worldPosition += movementDelta * movementSpeed * Time.deltaTime;
        }

        public Vector2 GetWorldPosition() {
            return worldPosition;
        }

        public void ImportValues(ICameraTarget otherTarget) {
            movementDelta = Vector2.zero;
            this.worldPosition = otherTarget.GetWorldPosition();
            this.orthoSize = otherTarget.GetOrthoSize();
        }
    }
}