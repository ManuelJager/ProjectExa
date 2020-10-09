using System;
using DG.Tweening;
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

        [Header("Settings")]
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color removeColor;
        [SerializeField] private Color infoColor;
        [SerializeField] private float cursorAnimTime = 0.25f;
        [SerializeField] private CursorAnimSettings animInSettings;
        [SerializeField] private CursorAnimSettings animOutSettings;

        [Header("Input")]
        [SerializeField] private InputAction inputAction;

        private CursorState cursorState = CursorState.idle;
        private Tween cursorColorTween;
        private Tween cursorClickAlphaTween;
        private Tween cursorRotationTween;
        private bool lockCursorScaleAnim;
        private FloatTweenBlender cursorScaleBlender;

        public MarkerContainer HoverMarkerContainer { get; private set; }
        public Vector2? ViewportPivot { get; set; }

        private void Start()
        {
            cursorScaleBlender = new FloatTweenBlender(1f, value =>
            {
                rectTransform.localScale = new Vector3(value, value, 1);
            });

            HoverMarkerContainer = new MarkerContainer(active =>
            {
                cursorScaleBlender.To(0, active ? 1.35f : 1f, cursorAnimTime);
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
            if (!ViewportPivot.HasValue) 
                return;

            var difference = ViewportPivot.Value - viewportPoint;

            if (lockCursorScaleAnim && difference.magnitude > 10f)
                lockCursorScaleAnim = false;

            if (lockCursorScaleAnim)
                return;

            var angle = (difference.GetAngle() + 270f - 22.5f) % 360;
            AnimCursorDirection(angle, 0.25f);
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
            cursorColorTween?.Kill();
            cursorColorTween = backgroundImage
                .DOColor(GetCursorColor(state), cursorAnimTime);
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
            AnimCursorSize(animInSettings);
            lockCursorScaleAnim = true;
            ViewportPivot = Systems.Input.ScaledViewportPoint;
        }

        private void OnLeftMouseCanceled(InputAction.CallbackContext context)
        {
            AnimCursorSize(animOutSettings);
            ViewportPivot = null;
            AnimCursorDirection(0f, 0.10f);
        }

        private void AnimCursorSize(CursorAnimSettings args)
        {
            cursorScaleBlender
                .To(1, args.sizeTarget, args.animTime)
                .SetEase(args.ease);

            cursorClickAlphaTween?.Kill();
            cursorClickAlphaTween = backgroundGroup
                .DOFade(args.alphaTarget, args.animTime)
                .SetEase(args.ease);
        }

        private void AnimCursorDirection(float angle, float time)
        {
            var targetVector = new Vector3(0, 0, angle);
            cursorRotationTween?.Kill();
            cursorRotationTween = normalCursor.DORotate(targetVector, time);
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
        private struct CursorAnimSettings
        {
            public float sizeTarget;
            public float alphaTarget;
            public float animTime;
            public Ease ease;
        }
    }
}