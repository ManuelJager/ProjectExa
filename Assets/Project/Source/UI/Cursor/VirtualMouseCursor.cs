using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Exa.Generics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Exa.UI
{
    public class VirtualMouseCursor : MonoBehaviour, ICursor
    {
        [SerializeField] private Image cursorBackground;
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color removeColor;
        [SerializeField] private Color infoColor;
        [SerializeField] private float cursorAnimTime = 0.25f;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform normalCursor;
        [SerializeField] private RectTransform inputCursor;
        [SerializeField] private InputAction inputAction;
        private readonly CursorState cursorState = CursorState.idle;
        private Tween cursorColorTween;
        private Tween cursorClickSizeTween;
        private Tween cursorHoverSizeTween;
        private float cursorClickSize = 1;
        private float cursorHoverSize = 1;

        public MarkerContainer HoverMarkerContainer { get; private set; }

        private void Start()
        {
            void SetClickSizeTarget(float target)
            {
                cursorClickSizeTween?.Kill();
                cursorClickSizeTween = DOTween.To(
                    () => cursorClickSize,
                    (x) => cursorClickSize = x,
                    target,
                    cursorAnimTime);
            }

            HoverMarkerContainer = new MarkerContainer((active) =>
            {
                cursorHoverSizeTween?.Kill();
                cursorHoverSizeTween = DOTween.To(
                    () => cursorHoverSize, 
                    (x) => cursorHoverSize = x,
                    active ? 1.2f : 1f,
                    cursorAnimTime);
            });

            inputAction.started += (context) => SetClickSizeTarget(0.7f);
            inputAction.canceled += (context) => SetClickSizeTarget(1f);
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
            rectTransform.anchoredPosition = Systems.Input.ScaledViewportPoint;
            var scale = cursorClickSize * cursorHoverSize;
            rectTransform.localScale = new Vector3(scale, scale, 1);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetState(CursorState state)
        {
            SwitchActive(cursorState, false);
            SwitchActive(state, true);
            cursorColorTween?.Kill();
            cursorColorTween = cursorBackground
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
    }
}