using System;
using DG.Tweening;
using Exa.Data;
using Exa.Math;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.UI.Cursor {
    [Serializable]
    public class StandardCursorFacade : VirtualCursorFacade {
        [Header("Color settings")]
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color removeColor;
        [SerializeField] private Color infoColor;
        [SerializeField] private float cursorColorAnimTime = 0.25f;

        [Header("Size anim settings")]
        [SerializeField] private float cursorScaleAnimTime = 0.25f;
        [SerializeField] private ActivePair<float> hoverableCursorSize;
        [SerializeField] private ActivePair<CursorSizeAnimSettings> sizeAnimSettings;

        [Header("Drag anim settings")]
        [SerializeField] private float cursorDragDistanceTolerance = 30f;
        [SerializeField] private ActivePair<CursorDragAnimSettings> dragAnimSettings;

        [Header("Input")]
        [SerializeField] private InputAction inputAction;
        
        private Tween cursorClickAlphaTween;
        private Tween cursorColorTween;

        private CursorDragState cursorDragState = CursorDragState.NotDragging;
        private FloatTweenBlender cursorScaleBlender;
        private CursorState cursorState = CursorState.idle;
        private CursorState? pendingCursorState;

        public Vector2? DragPivot { get; set; }

        public override void OnEnable() {
            base.OnEnable();
            inputAction.Enable();

            if (pendingCursorState.GetHasValue(out var value)) {
                SetState(value);

                pendingCursorState = null;
            } else {
                SetState(cursorState);
            }
            
            AnimCursorDirection(0, dragAnimSettings.active);
        }

        public override void OnDisable() {
            base.OnDisable();
            
            cursorClickAlphaTween?.Kill();
            cursorColorTween?.Kill();
            
            inputAction.Disable();
        }

        public override void Update(Vector2 viewportPoint) {
            cursorScaleBlender.Update();
            UpdateCursorScaleAnim(viewportPoint);
        }

        public void Init(CursorStateOverrideList overrides) {
            overrides.ContainsItemChange.AddListener(
                active => {
                    cursorScaleBlender.To(0, hoverableCursorSize.GetValue(active), cursorScaleAnimTime);
                });
            
            inputAction.started += OnLeftMouseStarted;
            inputAction.canceled += OnLeftMouseCanceled;

            cursorScaleBlender = new FloatTweenBlender(
                1f,
                value => mouse.rectTransform.localScale = new Vector3(value, value, 1),
                (current, blender) => current * blender
            );
        }

        public void SetState(CursorState state) {
            if (!Enabled) {
                pendingCursorState = state;

                return;
            }
            
            SwitchActive(cursorState, false);
            cursorState = state;
            SwitchActive(cursorState, true);

            mouse.backgroundImage.DOColor(GetCursorColor(state), cursorColorAnimTime)
                .Replace(ref cursorColorTween);
        }
        
         private void UpdateCursorScaleAnim(Vector2 viewportPoint) {
            if (!DragPivot.HasValue || cursorDragState == CursorDragState.NotDragging) {
                return;
            }

            var difference = viewportPoint - DragPivot.Value;

            if (cursorDragState == CursorDragState.BeginDragging && difference.magnitude > cursorDragDistanceTolerance) {
                cursorDragState = CursorDragState.Dragging;
            }

            if (cursorDragState == CursorDragState.Dragging) {
                AnimCursorDirection(DiffToAngle(difference), dragAnimSettings.active);
            }
        }
         
         private void OnLeftMouseStarted(InputAction.CallbackContext context) {
             AnimCursorSize(sizeAnimSettings.active);
             cursorDragState = CursorDragState.BeginDragging;
             DragPivot = S.Input.MouseScaledViewportPoint;
         }
 
         private void OnLeftMouseCanceled(InputAction.CallbackContext context) {
             AnimCursorSize(sizeAnimSettings.inactive);
             cursorDragState = CursorDragState.NotDragging;
             DragPivot = null;
             AnimCursorDirection(0f, dragAnimSettings.inactive);
         }
 
         private void AnimCursorSize(CursorSizeAnimSettings args) {
             cursorScaleBlender.To(1, args.sizeTarget, args.animTime)
                 .SetEase(args.ease);
 
             mouse.backgroundGroup.DOFade(args.alphaTarget, args.animTime)
                 .SetEase(args.ease)
                 .Replace(ref cursorClickAlphaTween);
         }

         private void SwitchActive(CursorState state, bool active) {
             switch (state) {
                 case CursorState.idle:
                 case CursorState.active:
                 case CursorState.remove:
                 case CursorState.info:
                     mouse.normalCursor.gameObject.SetActive(active);
                     break;
                 case CursorState.input:
                     mouse.inputCursor.gameObject.SetActive(active);
                     break;
                 default:
                     throw new ArgumentOutOfRangeException(nameof(state), state, null);
             }
         }
 
         private Color GetCursorColor(CursorState state) {
             return state switch {
                 CursorState.idle => idleColor,
                 CursorState.active => activeColor,
                 CursorState.remove => removeColor,
                 CursorState.info => infoColor,
                 _ => throw new ArgumentOutOfRangeException()
             };
         }
    }
}