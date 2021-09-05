using System;
using DG.Tweening;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Cursor {
    [Serializable]
    public class GaussCannonCursorFacade : VirtualCursorFacade {
        [Header("Anim settings")]
        [SerializeField] private float fillRecoverTime;
        [SerializeField] private Color color;
        [SerializeField] private float colorAnimTime;
        [SerializeField] private CursorDragAnimSettings trackFiringDirection;

        private Tween fillRecoverTween;
        private Tween colorTween;
        private GaussCannonBehaviour gaussCannon;

        public void Init(VirtualMouseCursor mouse, GaussCannonBehaviour gaussCannon) {
            base.Init(mouse);
            this.gaussCannon = gaussCannon;
        }

        public override void OnEnable() {
            base.OnEnable();
            
            fillRecoverTween?.Kill();

            mouse.backgroundImage.DOColor(color, colorAnimTime)
                .Replace(ref colorTween);
        }

        public void Update(float progress, bool coolingDown) {
            if (!Enabled) {
                return;
            }

            if (!coolingDown) {
                mouse.backgroundImage.fillAmount = progress;
                var angle = DiffToAngle(gaussCannon.transform.position.ToVector2() - S.Input.MouseWorldPoint);
                AnimCursorDirection(angle, trackFiringDirection);
            }
        }

        public override void OnDisable() {
            base.OnDisable();
            
            colorTween?.Kill();

            mouse.backgroundImage.DOFillAmount(1f, fillRecoverTime)
                .Replace(ref fillRecoverTween);
        }
    }
}