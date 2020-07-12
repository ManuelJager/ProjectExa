using UnityEngine;

namespace Exa.Gameplay
{
    public class CameraTarget : MonoBehaviour, ICameraTarget
    {
        public Vector2 GetWorldPosition()
        {
            return transform.position;
        }
    }
}