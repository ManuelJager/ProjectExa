using Exa.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class Radio : InputControl<bool>
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

        public void Toggle()
        {
            Value = !value;
        }
    }
}