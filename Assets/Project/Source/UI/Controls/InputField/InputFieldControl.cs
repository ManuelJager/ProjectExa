using Exa.UI.Components;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class InputFieldControl : InputControl<string>
    {
        [SerializeField] private Text placeholderText;
        [SerializeField] private ExtendedInputField inputField;

        public override string CleanValue { get; set; }

        public override string Value
        {
            get => inputField.text;
            set => inputField.text = value;
        }

        [SerializeField] private InputFieldEvent onValueChange = new InputFieldEvent();

        public override UnityEvent<string> OnValueChange => onValueChange;

        private void Awake()
        {
            inputField.onEndEdit.AddListener(onValueChange.Invoke);
        }

        public void SetValueWithoutNotify(string value)
        {
            inputField.SetTextWithoutNotify(value);
        }

        public void Setup(string valuePlaceholder, string value = "")
        {
            placeholderText.text = valuePlaceholder;
            inputField.text = value;
        }

        [Serializable]
        public class InputFieldEvent : UnityEvent<string>
        {
        }
    }
}