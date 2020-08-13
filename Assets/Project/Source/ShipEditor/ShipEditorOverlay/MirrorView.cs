using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.ShipEditor
{
    public class MirrorView : MonoBehaviour
    {
        [SerializeField] private KeybindingView keybindingView;
        [SerializeField] private Image mirrorImage;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;

        public void SetState(bool value)
        {
            mirrorImage.color = value ? activeColor : inactiveColor;
        }
    }
}