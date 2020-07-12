using UnityEngine;

namespace Exa.Gameplay
{
    public class UserCameraTarget : ICameraTarget
    {
        public Vector2 worldPosition;

        public Vector2 GetWorldPosition()
        {
            return worldPosition;
        }
    }
}