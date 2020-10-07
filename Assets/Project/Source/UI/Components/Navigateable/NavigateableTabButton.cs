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
        [SerializeField] private RectTransform _self;
        [SerializeField] private Image _image;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private float _activeHeight;
        [SerializeField] private float _inactiveHeight;
        [SerializeField] private float _delay;

        [Header("Content")]
        [SerializeField] public AnimatedTabContent content;

        public override void HandleExit(Navigateable target)
        {
            _self.DOSizeDelta(_self.sizeDelta.SetY(80), _delay);
            _image.DOColor(_inactiveColor, _delay);

            content.HandleExit(target is NavigateableTabButton button 
                    ? Vector2.left * (button.order > order).To1()
                    : Vector2.zero);
        }

        public override void HandleEnter(NavigationArgs args)
        {
            _self.DOSizeDelta(_self.sizeDelta.SetY(120), _delay);
            _image.DOColor(_activeColor, _delay);


            content.HandleEnter(args?.current is NavigateableTabButton button
                    ? Vector2.right * (button.order > order).To1()
                    : Vector2.zero);
        }
    }
}