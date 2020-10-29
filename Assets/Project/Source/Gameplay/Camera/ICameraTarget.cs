using UnityEngine;

namespace Exa.Gameplay
{
    public interface ICameraTarget
    {
        float ZoomScale { get; }

        bool TargetValid { get; }

        Vector2 GetWorldPosition();

        float GetBaseOrthoSize();

        float GetCalculatedOrthoSize();
        
        void OnScroll(float yScroll);
    }
}