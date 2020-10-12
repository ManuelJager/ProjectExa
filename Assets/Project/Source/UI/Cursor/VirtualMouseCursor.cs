using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using Exa.Data;
using Exa.Generics;
using Exa.Math;
using Exa.UI.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
#pragma warning disable CS0649

namespace Exa.UI
{
    public class VirtualMouseCursor : MonoBehaviour, ICursor
    {
        [Header("References")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private CanvasGroup backgroundGroup;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform normalCursor;
        [SerializeField] private RectTransform inputCursor;

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

        private CursorDragState cursorDragState = CursorDragState.NotDragging;
        private CursorState cursorState = CursorState.idle;
        private Tween cursorColorTween;
        private Tween cursorClickAlphaTween;
        private Tween cursorDragRotateTween;
        private FloatTweenBlender cursorScaleBlender;

        public Vector2? DragPivot { get; set; }

        public void Init()
        {
            cursorScaleBlender = new FloatTweenBlender(1f,
                value => rectTransform.localScale = new Vector3(value, value, 1),
                (current, blender) => current * blender);

            Systems.UI.mouseCursor.stateManager.ContainsItemChange.AddListener(active =>
            {
                cursorScaleBlender.To(0, hoverableCursorSize.GetValue(active), cursorScaleAnimTime);
            });

            inputAction.started += OnLeftMouseStarted;
            inputAction.canceled += OnLeftMouseCanceled;
        }

        private void OnEnable()
        {
            inputAction.Enable();
        }

        private void OnDisable()
        {
            inputAction.Disable();
        }

        private void Update()
        {
            var viewportPoint = Systems.Input.ScaledViewportPoint;
            rectTransform.anchoredPosition = viewportPoint;
            cursorScaleBlender.Update();
            UpdateCursorScaleAnim(viewportPoint);
        }

        private void UpdateCursorScaleAnim(Vector2 viewportPoint)
        {
            if (!DragPivot.HasValue || cursorDragState == CursorDragState.NotDragging) 
                return;

            var difference = DragPivot.Value - viewportPoint;


            if (cursorDragState == CursorDragState.BeginDragging && difference.magnitude > cursorDragDistanceTolerance)
                cursorDragState = CursorDragState.Dragging;

            if (cursorDragState == CursorDragState.Dragging)
            {
                var angle = (difference.GetAngle() + 270f - 22.5f) % 360;
                AnimCursorDirection(angle, dragAnimSettings.active);
            }
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetState(CursorState state)
        {
            SwitchActive(cursorState, false);
            SwitchActive(state, true);
            cursorState = state;
            backgroundImage
                .DOColor(GetCursorColor(state), cursorColorAnimTime);
        }

        public void OnEnterViewport()
        {
            gameObject.SetActive(true);
            Cursor.visible = false;
        }

        public void OnExitViewport()
        {
            gameObject.SetActive(false);
            Cursor.visible = true;
        }

        private void OnLeftMouseStarted(InputAction.CallbackContext context)
        {
            AnimCursorSize(sizeAnimSettings.active);
            cursorDragState = CursorDragState.BeginDragging;
            DragPivot = Systems.Input.ScaledViewportPoint;
        }

        private void OnLeftMouseCanceled(InputAction.CallbackContext context)
        {
            AnimCursorSize(sizeAnimSettings.inactive);
            cursorDragState = CursorDragState.NotDragging;
            DragPivot = null;
            AnimCursorDirection(0f, dragAnimSettings.inactive);
        }

        private void AnimCursorSize(CursorSizeAnimSettings args)
        {
            cursorScaleBlender
                .To(1, args.sizeTarget, args.animTime)
                .SetEase(args.ease);

            cursorClickAlphaTween?.Kill();
            cursorClickAlphaTween = backgroundGroup
                .DOFade(args.alphaTarget, args.animTime)
                .SetEase(args.ease);
        }

        private void AnimCursorDirection(float angle, CursorDragAnimSettings args)
        {
            var targetVector = new Vector3(0, 0, angle);
            cursorDragRotateTween?.Kill();
            cursorDragRotateTween = normalCursor
                .DORotate(targetVector, args.animTime)
                .SetEase(args.ease);
        }

        private void SwitchActive(CursorState state, bool active)
        {
            switch (state)
            {
                case CursorState.idle:
                case CursorState.active:
                case CursorState.remove:
                case CursorState.info:
                    normalCursor.gameObject.SetActive(active);
                    break;

                case CursorState.input:
                    inputCursor.gameObject.SetActive(active);
                    break;
            }
        }

        private Color GetCursorColor(CursorState state)
        {
            switch (state)
            {
                case CursorState.idle:
                    return idleColor;
                case CursorState.active:
                    return activeColor;
                case CursorState.remove:
                    return removeColor;
                case CursorState.info:
                    return infoColor;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Serializable]
        private struct CursorSizeAnimSettings
        {
            public float sizeTarget;
            public float alphaTarget;
            public float animTime;
            public Ease ease;
        }

        [Serializable]
        private struct CursorDragAnimSettings
        {
            public float animTime;
            public Ease ease;
        }

        private enum CursorDragState
        {
            NotDragging,
            BeginDragging,
            Dragging
        }
    }
}