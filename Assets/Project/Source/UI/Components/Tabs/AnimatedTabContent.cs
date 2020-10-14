using System;
using DG.Tweening;
using Exa.Data;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI.Components
{
    public class AnimatedTabContent : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] private float duration = 0.25f;
        [SerializeField] private ActivePair<AnimationArgs> animArgs;

        private TweenRef<float> alphaTween;
        private TweenRef<Vector2> positionTween;

        public void HandleEnter(Vector2 direction)
        {
            gameObject.SetActive(true);
            Animate(animArgs.active, direction * animArgs.active.animAmplitude, Vector2.zero);
            this.Delay(() => canvasGroup.interactable = true, duration);
        }

        public void HandleExit(Vector2 direction)
        {
            canvasGroup.interactable = false;
            Animate(animArgs.inactive, Vector2.zero, direction * animArgs.inactive.animAmplitude);
            this.Delay(() => gameObject.SetActive(false), duration);
        }

        private void Animate(AnimationArgs args, Vector2 initialPos, Vector2 targetPos)
        {
            alphaTween = alphaTween ?? new TweenWrapper<float>(canvasGroup.DOFade);
            alphaTween.To(args.targetAlpha, duration)
                .SetEase(args.ease);

            rectTransform.anchoredPosition = initialPos;
            positionTween = positionTween ?? new TweenWrapper<Vector2>(rectTransform.DOAnchorPos);
            positionTween.To(targetPos, duration)
                .SetEase(args.ease);
        }

        [Serializable]
        public struct AnimationArgs
        {
            public float targetAlpha;
            public float animAmplitude;
            public Ease ease;
        }
    }
}