using DG.Tweening;
using Exa.Math;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private float activeHeight;
        [SerializeField] private float inactiveHeight;
        [SerializeField] private float activeFontSize;
        [SerializeField] private float inactiveFontSize;
        [SerializeField] private float currentFontSize;
        [SerializeField] private float delay;

        private Tween fontTween;

        private float CurrentFontSize
        {
            get => currentFontSize;
            set
            {
                currentFontSize = value;
                text.fontSize = Mathf.RoundToInt(value);
            }
        }

        private void Awake()
        {
            CurrentFontSize = inactiveFontSize;
        }

        public override void HandleExit(Navigateable target)
        {
            self.DOSizeDelta(self.sizeDelta.SetY(80), delay);
            image.DOColor(inactiveColor, delay);

            content.HandleExit(target is NavigateableTabButton button 
                    ? Vector2.left * (button.order > order).To1()
                    : Vector2.zero);

            TweenText(inactiveFontSize);
        }

        public override void HandleEnter(NavigationArgs args)
        {
            self.DOSizeDelta(self.sizeDelta.SetY(120), delay);
            image.DOColor(activeColor, delay);

            content.HandleEnter(args?.current is NavigateableTabButton button
                    ? Vector2.right * (button.order > order).To1()
                    : Vector2.zero);

            TweenText(activeFontSize);
        }

        private void TweenText(float target)
        {
            fontTween?.Kill();
            fontTween = DOTween.To(
                () => CurrentFontSize,
                (x) => CurrentFontSize = x,
                target,
                delay);
        }
    }
}