using Exa.UI.Components;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class InputFieldControl : InputControl<string>
    {
        public Text placeholderText;
        public ExtendedInputField inputField;

        public override string CleanValue { get; set; }

        public override string Value
        {
            get => inputField.text;
            set => inputField.text = value;
        }

        [SerializeField] private readonly InputFieldEvent _onValueChange = new InputFieldEvent();

        public override UnityEvent<string> OnValueChange => _onValueChange;

        private void Awake()
        {
            inputField.onEndEdit.AddListener(_onValueChange.Invoke);
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