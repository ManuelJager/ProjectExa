using Exa.UI.Components;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Slider;

namespace Exa.UI.Controls
{
    public class ExaSlider : InputControl<float>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private ExtendedInputField inputField;

        public SliderEvent onValueChanged = new SliderEvent();

        public override float CleanValue { get; set; }
        public override float Value 
        { 
            get => slider.value;
            set
            {
                slider.value = value;
                inputField.text = FormatFloat(value);
            }
        }

        private void Awake()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            slider.onValueChanged.AddListener(onValueChanged.Invoke);
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
            inputField.text = FormatFloat(slider.value);
        }

        private void OnSliderValueChanged(float value)
        {
            inputField.text = FormatFloat(value);
        }

        private void OnInputFieldValueChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            float floatValue;
            var valid = float.TryParse(value, out floatValue);

            if (!valid)
            {
                inputField.text = FormatFloat(slider.value);
                return;
            }

            if (floatValue < slider.minValue) inputField.text = FormatFloat(slider.minValue);
            if (floatValue > slider.maxValue) inputField.text = FormatFloat(slider.maxValue);
        }

        private void OnInputFieldEndEdit(string value)
        {
            float floatValue;
            var valid = float.TryParse(value, out floatValue);

            if (valid)
            {
                slider.value = (float)Math.Round(floatValue, 2);
            }
            else
            {
                inputField.text = FormatFloat(slider.value);
            }
        }

        private string FormatFloat(float value)
        {
            return Math.Round(value, 2).ToString();
        }
    }
}