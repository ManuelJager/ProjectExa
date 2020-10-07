using Exa.Math;
using System.Collections;
using UnityEngine;

namespace Exa.UI.Components
{
    public enum AnimationDirection
    {
        None = 0,
        Top = 1,
        Left = 2,
        Bottom = 3,
        Right = 4,
    }

    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class UiAnimateable : MonoBehaviour
    {
        public float msLocalAnimationOffset = 0f;

        // movement animation
        public AnimationDirection movementDirection = AnimationDirection.None;

        public float movementSmoothDamp = 0.1f;
        public float movementMagnitude = 20f;

        // Alpha animation
        public bool animateAlpha;

        public float alphaSpeed = 8f;
        private float _originalAlpha = 0f;

        private Vector2 _elementVelocity = Vector2.zero;
        private Vector2 _originalPos;

        private CanvasGroup _canvasGroup;
        private RectTransform _rect;

        private float Alpha
        {
            get => _canvasGroup.alpha;
            set => _canvasGroup.alpha = Mathf.Clamp(value, 0, 1);
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rect = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _originalAlpha = Alpha;

            if (animateAlpha)
            {
                Alpha = 0f;
                StartDelayedCoroutine(msLocalAnimationOffset / 1000f, FadeIn(_originalAlpha));
            }

            if (movementDirection == AnimationDirection.None) return;

            _originalPos = _rect.anchoredPosition;

            // Create a vector that points upwards and rotate it by the animation direction
            // This is used as an offset from the original rect position
            var offset = new Vector2
            {
                x = 0,
                y = movementMagnitude
            }.Rotate(movementDirection.GetRotation());

            _rect.anchoredPosition = _originalPos + offset;

            StartDelayedCoroutine(msLocalAnimationOffset / 1000f, SlideIn(_originalPos));
        }

        private void OnDisable()
        {
            SkipAnimations();
        }

        public void SkipAnimations()
        {
            StopAllCoroutines();

            Alpha = _originalAlpha;

            if (movementDirection == AnimationDirection.None) return;

            _rect.anchoredPosition = _originalPos;
        }

        private IEnumerator FadeIn(float target)
        {
            if (Alpha < target)
            {
                Alpha += Time.deltaTime * alphaSpeed;

                yield return null;

                StartCoroutine(FadeIn(target));
            }
        }

        private IEnumerator SlideIn(Vector2 towards)
        {
            if (_rect.anchoredPosition != towards)
            {
                _rect.anchoredPosition = Vector2.SmoothDamp(
                    _rect.anchoredPosition,
                    towards,
                    ref _elementVelocity,
                    movementSmoothDamp);

                yield return null;

                StartCoroutine(SlideIn(towards));
            }
        }

        private void StartDelayedCoroutine(float seconds, IEnumerator routine)
        {
            StartCoroutine(DelayCoroutine(seconds, routine));
        }

        private IEnumerator DelayCoroutine(float seconds, IEnumerator routine)
        {
            if (seconds != 0f)
            {
                yield return new WaitForSeconds(seconds);
            }
            StartCoroutine(routine);
        }
    }
}