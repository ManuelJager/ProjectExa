using DG.Tweening;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

namespace Exa.UI
{
    public class RootNavigationContent : MonoBehaviour
    {
        [SerializeField] private LayoutElement tooltipContainer;
        [SerializeField] private TextAnimator textAnimator;

        private TweenRef<float> heightTween;

        private void Awake()
        {
            heightTween = new TweenWrapper<float>(tooltipContainer.DOPreferredHeight)
                .SetDuration(0.15f);

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
            heightTween.To(active ? 60f : 0f);
        }
    }
}