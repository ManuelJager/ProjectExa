using Exa.UI.Components;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    [Serializable]
    public class RadioControl : InputControl<bool>
    {
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private Color _activeColor;
        private bool _value;

        public override bool CleanValue { get; set; }

        public override bool Value
        {
            get => _value;
            set
            {
                this._value = value;

                _buttonImage.color = value ? _activeColor : _inactiveColor;
            }
        }

        private RadioCheckEvent _onValueChange = new RadioCheckEvent();

        public override UnityEvent<bool> OnValueChange 
        { 
            get => _onValueChange; 
        }

        public void Toggle()
        {
            Value = !_value;
        }

        [Serializable]
        public class RadioCheckEvent : UnityEvent<bool>
        {
        }
    }
}