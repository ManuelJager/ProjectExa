using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void SetText(string value)
        {
            text.text = value;
        }

        public void SetFont(Font font)
        {
            text.font = font;
        }
    }
}