using DG.Tweening;
using Exa.Math;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Cursor {
    public abstract class VirtualCursorFacade {
        protected Tween cursorDragRotateTween;
        protected VirtualMouseCursor mouse;
        
        public bool Enabled { get; protected set; }
        
        protected void Init(VirtualMouseCursor mouse) {
            this.mouse = mouse;
        }
        
        protected void AnimCursorDirection(float angle, CursorDragAnimSettings args) {
            var targetVector = new Vector3(0, 0, angle);

            mouse.normalCursor.DORotate(targetVector, args.animTime)
                .SetEase(args.ease)
                .Replace(ref cursorDragRotateTween);
        }
        
        protected float DiffToAngle(Vector2 diff) {
            return (diff.GetAngle() + 90f - 22.5f) % 360f;
        }

        public virtual void OnEnable() {
            Enabled = true;
        }

        public virtual void OnDisable() {
            Enabled = false;
            cursorDragRotateTween?.Kill();
        }

        public virtual void Update(Vector2 viewportPoint) { }
    }
}