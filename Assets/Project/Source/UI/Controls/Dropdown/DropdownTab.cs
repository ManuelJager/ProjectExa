using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    [RequireComponent(typeof(Button))]
    public class DropdownTab : MonoBehaviour
    {
        public Button button;

        [SerializeField] private Text _text;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        private void Awake()
        {
            Selected = false;
        }

        public string Text
        {
            set => _text.text = value;
        }

        public bool Selected
        {
            set => _text.color = value ? _activeColor : _inactiveColor;
        }
    }
}