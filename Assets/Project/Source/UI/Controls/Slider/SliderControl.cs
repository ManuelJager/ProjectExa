using Exa.Generics;
using Exa.UI.Components;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Slider;

namespace Exa.UI.Controls
{
    public class SliderControl : InputControl<float>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private ExtendedInputField inputField;
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

        [SerializeField] private readonly SliderEvent onValueChanged = new SliderEvent();

        public override UnityEvent<float> OnValueChange => onValueChanged;

        private void Awake()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            slider.onValueChanged.AddListener(onValueChanged.Invoke);
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
            inputField.text = FormatFloat(slider.value);
        }

        public void SetMinMax(MinMax<float> minMax)
        {
            slider.minValue = minMax.min;
            slider.maxValue = minMax.max;
        }

        private void OnSliderValueChanged(float value)
        {
            inputField.text = FormatFloat(value);
        }

        private void OnInputFieldValueChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            var valid = float.TryParse(value, out var floatValue);

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
            var valid = float.TryParse(value, out var floatValue);

            if (valid)
            {
                slider.value = (float)System.Math.Round(floatValue, 2);
                onValueChanged.Invoke(floatValue);
            }
            else
            {
                inputField.text = FormatFloat(slider.value);
            }
        }

        private string FormatFloat(float value)
        {
            return System.Math.Round(value, 2).ToString();
        }
    }
}