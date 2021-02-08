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
        public static RadioControl Create(Transform container, string label, Action<bool> setter) {
            return Systems.UI.controlFactory.CreateRadio(container, label, setter);
        }
        
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