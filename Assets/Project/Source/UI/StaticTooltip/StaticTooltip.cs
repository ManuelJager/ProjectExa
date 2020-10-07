using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class StaticTooltip : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void DisplayTooltip(string message)
        {
            if (message == "")
            {
                gameObject.SetActive(false);
                return;
            }

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            _text.text = message;
        }
    }
}