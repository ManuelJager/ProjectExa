using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    [RequireComponent(typeof(Button))]
    public class DropdownTab : MonoBehaviour
    {
        public Button button;

        [SerializeField] private Text text;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;

        private void Awake()
        {
            Selected = false;
        }

        public string Text
        {
            set
            {
                text.text = value;
            }
        }

        public bool Selected
        {
            set
            {
                text.color = value ? activeColor : inactiveColor;
            }
        }
    }
}