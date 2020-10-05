using DG.Tweening;
using Exa.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class NavigateableTabButton : Navigateable
    {
        public int order;

        [Header("Button")]
        [SerializeField] private RectTransform self;
        [SerializeField] private Image image;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private float activeHeight;
        [SerializeField] private float inactiveHeight;
        [SerializeField] private float delay;

        [Header("Content")]
        [SerializeField] public AnimatedTabContent content;

        public override void HandleExit(Navigateable target)
        {
            self.DOSizeDelta(self.sizeDelta.SetY(80), delay);
            image.DOColor(inactiveColor, delay);

            content.HandleExit(target is NavigateableTabButton button 
                    ? Vector2.left * (button.order > order).To1()
                    : Vector2.zero);
        }

        public override void HandleEnter(NavigationArgs args)
        {
            self.DOSizeDelta(self.sizeDelta.SetY(120), delay);
            image.DOColor(activeColor, delay);


            content.HandleEnter(args?.current is NavigateableTabButton button
                    ? Vector2.right * (button.order > order).To1()
                    : Vector2.zero);
        }
    }
}