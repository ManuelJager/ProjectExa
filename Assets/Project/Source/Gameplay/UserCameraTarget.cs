using UnityEngine;

namespace Exa.Gameplay
{
    public class UserCameraTarget : ICameraTarget
    {
        public Vector2 worldPosition;

        public Vector3 GetWorldPosition()
        {
            return worldPosition;
        }
    }
}