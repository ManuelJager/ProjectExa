using Exa.Math;
using UnityEngine;

namespace Exa.Gameplay
{
    public abstract class CameraTarget : ICameraTarget
    {
        public float ZoomScale { get; protected set; } = 1f;

        public virtual bool TargetValid => true;

        public virtual float GetCalculatedOrthoSize() {
            return GetBaseOrthoSize() * ZoomScale;
        }

        public abstract Vector2 GetWorldPosition();

        public abstract float GetBaseOrthoSize();

        public void OnScroll(float yScroll) {
            ZoomScale -= yScroll / 500f;
            ZoomScale = Mathf.Clamp(ZoomScale, 0.5f, 2.5f);
        }
    }
}