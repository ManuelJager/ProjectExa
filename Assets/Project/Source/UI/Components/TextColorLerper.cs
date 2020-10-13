using DG.Tweening;
using Exa.Data;
using Exa.UI.Tweening;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

namespace Exa.UI.Components
{
    public class TextColorLerper : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private ActivePair<Color> color;

        private TweenRef<Color> colorTween;

        private void Awake()
        {
            colorTween = new TweenWrapper<Color>(text.DOColor)
                .DODefaultDuration(0.1f);
        }

        public void SetColor(bool active)
        {
            colorTween.To(color.GetValue(active), 0.1f);
        }
    }
}