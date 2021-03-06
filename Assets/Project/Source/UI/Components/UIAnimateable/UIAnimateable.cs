﻿using Exa.Math;
using System.Collections;
using UnityEngine;

namespace Exa.UI.Components
{
    public enum AnimationDirection
    {
        none = 0,
        top = 1,
        left = 2,
        bottom = 3,
        right = 4,
    }

    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class UIAnimateable : MonoBehaviour
    {
        public float msLocalAnimationOffset = 0f;

        // movement animation
        public AnimationDirection movementDirection = AnimationDirection.none;

        public float movementSmoothDamp = 0.1f;
        public float movementMagnitude = 20f;

        // Alpha animation
        public bool animateAlpha;

        public float alphaSpeed = 8f;
        private float originalAlpha = 0f;

        private Vector2 elementVelocity = Vector2.zero;
        private Vector2 originalPos;

        private CanvasGroup canvasGroup;
        private RectTransform rect;

        private float Alpha {
            get => canvasGroup.alpha;
            set => canvasGroup.alpha = Mathf.Clamp(value, 0, 1);
        }

        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            rect = GetComponent<RectTransform>();
        }

        private void OnEnable() {
            originalAlpha = Alpha;

            if (animateAlpha) {
                Alpha = 0f;
                StartDelayedCoroutine(msLocalAnimationOffset / 1000f, FadeIn(originalAlpha));
            }

            if (movementDirection == AnimationDirection.none) return;

            originalPos = rect.anchoredPosition;

            // Create a vector that points upwards and rotate it by the animation direction
            // This is used as an offset from the original rect position
            var offset = new Vector2 {
                x = 0,
                y = movementMagnitude
            }.Rotate(movementDirection.GetRotation());

            rect.anchoredPosition = originalPos + offset;

            StartDelayedCoroutine(msLocalAnimationOffset / 1000f, SlideIn(originalPos));
        }

        private void OnDisable() {
            SkipAnimations();
        }

        public void SkipAnimations() {
            StopAllCoroutines();

            Alpha = originalAlpha;

            if (movementDirection == AnimationDirection.none) return;

            rect.anchoredPosition = originalPos;
        }

        private IEnumerator FadeIn(float target) {
            if (Alpha < target) {
                Alpha += Time.deltaTime * alphaSpeed;

                yield return null;

                StartCoroutine(FadeIn(target));
            }
        }

        private IEnumerator SlideIn(Vector2 towards) {
            if (rect.anchoredPosition != towards) {
                rect.anchoredPosition = Vector2.SmoothDamp(
                    rect.anchoredPosition,
                    towards,
                    ref elementVelocity,
                    movementSmoothDamp);

                yield return null;

                StartCoroutine(SlideIn(towards));
            }
        }

        private void StartDelayedCoroutine(float seconds, IEnumerator routine) {
            StartCoroutine(DelayCoroutine(seconds, routine));
        }

        private IEnumerator DelayCoroutine(float seconds, IEnumerator routine) {
            if (seconds != 0f) {
                yield return new WaitForSeconds(seconds);
            }

            StartCoroutine(routine);
        }
    }
}