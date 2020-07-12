using UnityEngine;

namespace Exa.Gameplay
{
    public class CameraTarget : MonoBehaviour, ICameraTarget
    {
        public float GetOrthoSize()
        {
            return 30f;
        }

        public Vector2 GetWorldPosition()
        {
            return transform.position;
        }
    }
}