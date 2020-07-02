using Exa.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class ExaInputField : InputControl<string>
    {
        public Text nameText;
        public Text placeholderText;
        public ExtendedInputField inputField;

        public override string CleanValue { get; set; }
        public override string Value 
        { 
            get => inputField.text; 
            set => inputField.text = value; 
        }

        public void Setup(string name, string valuePlaceholder, string value = "")
        {
            nameText.text = name;
            placeholderText.text = valuePlaceholder;
            inputField.text = value;
        }
    }
}