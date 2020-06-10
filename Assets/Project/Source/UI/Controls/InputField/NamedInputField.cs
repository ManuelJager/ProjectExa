using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class NamedInputField : MonoBehaviour
    {
        public Text nameText;
        public Text placeholderText;
        public ExaInputField inputField;

        public void Setup(string name, string valuePlaceholder, string value = "")
        {
            nameText.text = name;
            placeholderText.text = valuePlaceholder;
            inputField.text = value;
        }
    }
}