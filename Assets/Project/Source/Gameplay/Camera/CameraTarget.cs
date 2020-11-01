using Exa.Generics;
using Exa.Math;
using UnityEngine;

namespace Exa.Gameplay
{
    public abstract class CameraTarget : ICameraTarget
    {
        private CameraTargetSettings settings;
        private BezierCurve zoomCurve;

        public float ZoomScale { get; protected set; } = 0.5f;
        public virtual bool TargetValid => true;

        protected CameraTarget(CameraTargetSettings settings)
        {
            this.settings = settings;
            zoomCurve = new BezierCurve(settings.bezierCurveSettings);
        }

        public virtual float GetCalculatedOrthoSize() {
            var multiplier = settings.zoomMultiplier.Evaluate(zoomCurve.GetY(ZoomScale));
            return GetBaseOrthoSize() * multiplier;
        }

        public abstract Vector2 GetWorldPosition();

        public abstract float GetBaseOrthoSize();

        public void OnScroll(float yScroll) {
            ZoomScale -= yScroll / 1000f * settings.zoomSpeed;
            ZoomScale = Mathf.Clamp01(ZoomScale);
        }
    }
}