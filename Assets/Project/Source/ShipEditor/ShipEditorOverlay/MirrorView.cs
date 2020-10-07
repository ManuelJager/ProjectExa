using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.ShipEditor
{
    public class MirrorView : MonoBehaviour
    {
        [SerializeField] private KeybindingView _keybindingView;
        [SerializeField] private Image _mirrorImage;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        public void SetState(bool value)
        {
            _mirrorImage.color = value ? _activeColor : _inactiveColor;
        }
    }
}