using UnityEngine;

namespace Exa.Gameplay
{
    public class CameraTarget : MonoBehaviour, ICameraTarget
    {
        public Vector3 GetWorldPosition()
        {
            return transform.position;
        }
    }
}