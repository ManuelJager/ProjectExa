using System;
using DG.Tweening;
using Exa.Data;
using Exa.Math;
using Exa.Utils;
using Exa.UI.Tweening;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

namespace Exa.UI.Components
{
    public class NavigateableTabButton : Navigateable
    {
        public int order;

        [Header("References")]
        [SerializeField] private RectTransform self;
        [SerializeField] private Text text;
        [SerializeField] private Image image;
        [SerializeField] private AnimatedTabContent content;

        [Header("Settings")]
        [SerializeField] private float duration;
        [SerializeField] private ActivePair<AnimationArgs> animArgs;

        private TweenRef<Vector2> rectTween;
        private TweenRef<Color> imageTween;
        private TweenRef<int> fontTween;

        private void Awake()
        {
            rectTween = new TweenWrapper<Vector2>(self.DOSizeDelta)
                .SetDuration(duration);
            imageTween = new TweenWrapper<Color>(image.DOColor)
                .SetDuration(duration);
            fontTween = new IntTween()
                .DOGetter(() => text.fontSize)
                .DOSetter(x => text.fontSize = x)
                .SetDuration(duration);
        }

        public override void HandleExit(Navigateable target)
        {
            content.HandleExit(target is NavigateableTabButton button 
                    ? Vector2.left * (button.order > order).To1()
                    : Vector2.zero);

            Animate(animArgs.inactive);
        }

        public override void HandleEnter(NavigationArgs args)
        {
            content.HandleEnter(args?.current is NavigateableTabButton button
                    ? Vector2.right * (button.order > order).To1()
                    : Vector2.zero);

            Animate(animArgs.active);
        }

        private void Animate(AnimationArgs args)
        {
            rectTween.To(self.sizeDelta.SetY(args.height));
            imageTween.To(args.color);
            fontTween.To(args.fontSize);
        }

        [Serializable]
        private struct AnimationArgs
        {
            public Color color;
            public float height;
            public int fontSize;
        }
    }
}