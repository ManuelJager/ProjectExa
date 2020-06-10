using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private Text text;

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

            text.text = message;
        }
    }
}