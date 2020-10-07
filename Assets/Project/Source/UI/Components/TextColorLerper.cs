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

        public void SetColor(bool active)
        {
            text.DOColor(active ? activeColor : inactiveColor, 0.1f);
        }
    }
}