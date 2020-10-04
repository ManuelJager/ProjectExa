using DG.Tweening;
using Exa.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class NavigateableTabButton : Navigateable
    {
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

        public override void HandleExit()
        {
            self.DOSizeDelta(self.sizeDelta.SetY(80), delay);
            image.DOColor(inactiveColor, delay);
            content.HandleExit();
        }

        public override void HandleEnter(Navigateable from, NavigationArgs args = default)
        {
            self.DOSizeDelta(self.sizeDelta.SetY(120), delay);
            image.DOColor(activeColor, delay);
            content.HandleEnter();
        }
    }
}