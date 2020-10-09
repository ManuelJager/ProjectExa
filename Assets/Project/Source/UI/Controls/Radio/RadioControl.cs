using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    [Serializable]
    public class RadioControl : InputControl<bool>
    {
        [SerializeField] private Image buttonImage;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
        private bool value;

        public override bool CleanValue { get; set; }

        public override bool Value
        {
            get => value;
            set
            {
                this.value = value;

                buttonImage.color = value ? activeColor : inactiveColor;
            }
        }

        private RadioCheckEvent onValueChange = new RadioCheckEvent();

        public override UnityEvent<bool> OnValueChange 
        { 
            get => onValueChange; 
        }

        public void Toggle()
        {
            Value = !value;
        }

        [Serializable]
        public class RadioCheckEvent : UnityEvent<bool>
        {
        }
    }
}