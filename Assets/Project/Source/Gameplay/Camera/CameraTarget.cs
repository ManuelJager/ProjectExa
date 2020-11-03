using Exa.Generics;
using Exa.Math;
using UnityEngine;

namespace Exa.Gameplay
{
    public abstract class CameraTarget : ICameraTarget
    {
        private CameraTargetSettings settings;
        private BezierCurve zoomCurve;
        private float zoomScale;

        public float ZoomScale {
            get => zoomScale;
            set => zoomScale = Mathf.Clamp01(value);
        }

        public virtual bool TargetValid => true;

        protected CameraTarget(CameraTargetSettings settings)
        {
            this.settings = settings;
            zoomCurve = new BezierCurve(settings.bezierCurveSettings);
            ZoomScale = 0.5f;
        }

        public virtual float GetCalculatedOrthoSize() {
            var multiplier = settings.zoomMinMax.Evaluate(zoomCurve.GetY(ZoomScale));
            return settings.orthoMultiplier * multiplier;
        }

        public abstract Vector2 GetWorldPosition();

        public void OnScroll(float yScroll) {
            ZoomScale -= yScroll / 1000f * settings.zoomSpeed;
        }

        protected void SetZoomFromOrtho(float orthoSize) {
            ZoomScale = orthoSize / settings.orthoMultiplier;
        }
    }
}