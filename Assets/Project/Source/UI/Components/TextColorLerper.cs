using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class TextColorLerper : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        public void SetColor(bool active)
        {
            _text.DOColor(active ? _activeColor : _inactiveColor, 0.1f);
        }
    }
}