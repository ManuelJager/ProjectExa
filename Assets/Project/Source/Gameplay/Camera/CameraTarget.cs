using UnityEngine;

namespace Exa.Gameplay
{
    public class CameraTarget : MonoBehaviour, ICameraTarget
    {
        public float GetOrthoSize() {
            return 30f;
        }

        public bool GetTargetValid() {
            return true;
        }

        public Vector2 GetWorldPosition() {
            return transform.position;
        }
    }
}