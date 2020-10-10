using System;
using DG.Tweening;
using Exa.Utils;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI.Components
{
    public class AnimatedTabContent : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float animTime = 0.25f;

        [SerializeField] private AnimationArgs animEnterArgs = new AnimationArgs
        {
            animAmplitude = 120,
            targetAlpha = 1,
            ease = Ease.InCubic
        };
        [SerializeField] private AnimationArgs animExitArgs = new AnimationArgs
        {
            animAmplitude = 120,
            targetAlpha = 0,
            ease = Ease.OutCubic
        };

        private Tween alphaTween;
        private Tween positionTween;

        public void HandleEnter(Vector2 direction)
        {
            gameObject.SetActive(true);
            Animate(animEnterArgs, direction * animEnterArgs.animAmplitude, Vector2.zero);
            this.Delay(() => canvasGroup.interactable = true, animTime);
        }

        public void HandleExit(Vector2 direction)
        {
            canvasGroup.interactable = false;
            Animate(animExitArgs, Vector2.zero, direction * animExitArgs.animAmplitude);
            this.Delay(() => gameObject.SetActive(false), animTime);
        }

        private void Animate(AnimationArgs args, Vector2 initialPos, Vector2 targetPos)
        {
            alphaTween?.Kill();
            alphaTween = canvasGroup.DOFade(args.targetAlpha, animTime)
                .SetEase(args.ease);

            rectTransform.anchoredPosition = initialPos;
            positionTween?.Kill();
            positionTween = rectTransform.DOAnchorPos(targetPos, animTime)
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