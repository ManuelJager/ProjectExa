using System;
using Exa.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    [Serializable]
    public class RadioControl : InputControl<bool>
    {
        [SerializeField] private Image buttonImage;
        [SerializeField] private ActivePair<Color> colors;
        private bool value;

        public override bool Value {
            get => value;
            protected set {
                this.value = value;
                buttonImage.color = colors.GetValue(value);
            }
        }

        private RadioCheckEvent onValueChange = new RadioCheckEvent();

        public override UnityEvent<bool> OnValueChange {
            get => onValueChange;
        }

        public void Toggle() {
            Value = !value;
        }

        [Serializable]
        public class RadioCheckEvent : UnityEvent<bool>
        { }
    }
}