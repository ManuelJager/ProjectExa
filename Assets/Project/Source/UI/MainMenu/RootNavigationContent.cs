using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

namespace Exa.UI
{
    public class RootNavigationContent : MonoBehaviour
    {
        [SerializeField] private LayoutElement tooltipContainer;
        [SerializeField] private TextAnimator textAnimator;

        private Tween tooltipContainerTween;

        private void Awake()
        {
            RemoveLock();
        }

        public void RemoveLock()
        {
            Animate(false);
        }

        public void SetTooltip(string value)
        {
            textAnimator.AnimateTo(value);
            Animate(true);
        }

        private void Animate(bool active)
        {
            tooltipContainerTween?.Kill();
            tooltipContainerTween = tooltipContainer
                .DOPreferredSize(active ? new Vector2(0, 60) : Vector2.zero, 0.15f);
        }
    }
}