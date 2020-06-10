using UnityEngine;
using UnityEngine.UI;

namespace Exa.Ui.Controls
{
    [RequireComponent(typeof(Button))]
    public class DropdownTab : MonoBehaviour
    {
        public Button button;

        [SerializeField] private Text text;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;

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