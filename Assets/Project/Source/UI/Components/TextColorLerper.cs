using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class TextColorLerper : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;

        private Tween colorTween;

        public void SetColor(bool active)
        {
            colorTween?.Kill();
            colorTween = text.DOColor(active ? activeColor : inactiveColor, 0.1f);
        }
    }
}