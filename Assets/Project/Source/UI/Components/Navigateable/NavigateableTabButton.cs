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

            if (target is NavigateableTabButton button)
            {
                content.HandleExit(button.order > order ? Vector2.left : Vector2.right);
            }
            else
            {
                content.HandleExit(Vector2.up);
            }
        }

        public override void HandleEnter(NavigationArgs args)
        {
            self.DOSizeDelta(self.sizeDelta.SetY(120), delay);
            image.DOColor(activeColor, delay);

            if (args?.current is NavigateableTabButton button)
            {
                content.HandleEnter(button.order > order ? Vector2.right : Vector2.left);
            }
            else
            {
                content.HandleEnter(Vector2.down);
            }
        }
    }
}