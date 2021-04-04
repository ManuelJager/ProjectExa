﻿using Exa.Math;
using Exa.Types.Generics;
using Exa.UI.Tweening;
using UnityEngine;

namespace Exa.Camera
{
    public abstract class CameraTarget : ICameraTarget
    {
        private CameraTargetSettings settings;
        private ExaEase zoomCurve;
        private float zoomScale;

        public float ZoomScale {
            get => zoomScale;
            set => zoomScale = Mathf.Clamp01(value);
        }

        public virtual bool TargetValid => true;

        protected CameraTarget(CameraTargetSettings settings) {
            this.settings = settings;
            ZoomScale = 0.5f;
        }

        public virtual float GetCalculatedOrthoSize() {
            var easedZoomScale = settings.ZoomEase.Evaluate(ZoomScale);
            var multiplier = settings.zoomMinMax.Evaluate(easedZoomScale);
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