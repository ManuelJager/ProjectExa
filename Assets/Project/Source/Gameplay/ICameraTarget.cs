using UnityEngine;

namespace Exa.Gameplay
{
    public interface ICameraTarget
    {
        Vector2 GetWorldPosition();

        float GetOrthoSize();

        bool GetTargetValid();
    }
}