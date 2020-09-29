using DG.Tweening;
using Exa.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class NavigateableTabButton : Navigateable
    {
        [SerializeField] private RectTransform self;
        [SerializeField] private Transform content;
        [SerializeField] private Image image;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private float activeHeight;
        [SerializeField] private float inactiveHeight;

        public override void OnExit()
        {
            content.gameObject.SetActive(false);
            self.DOSizeDelta(self.sizeDelta.SetY(80), 0.2f);
            image.DOColor(inactiveColor, 0.2f);
        }

        public override void OnNavigate(Navigateable from, bool storeFrom = true)
        {
            content.gameObject.SetActive(true);
            self.DOSizeDelta(self.sizeDelta.SetY(120), 0.2f);
            image.DOColor(activeColor, 0.2f);
        }
    }
}