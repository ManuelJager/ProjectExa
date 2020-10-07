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
        [SerializeField] private Slider _slider;
        [SerializeField] private ExtendedInputField _inputField;
        public override float CleanValue { get; set; }

        public override float Value
        {
            get => _slider.value;
            set
            {
                _slider.value = value;
                _inputField.text = FormatFloat(value);
            }
        }

        [SerializeField] private readonly SliderEvent _onValueChanged = new SliderEvent();

        public override UnityEvent<float> OnValueChange => _onValueChanged;

        private void Awake()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            _slider.onValueChanged.AddListener(_onValueChanged.Invoke);
            _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            _inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
            _inputField.text = FormatFloat(_slider.value);
        }

        public void SetMinMax(MinMax<float> minMax)
        {
            _slider.minValue = minMax.min;
            _slider.maxValue = minMax.max;
        }

        private void OnSliderValueChanged(float value)
        {
            _inputField.text = FormatFloat(value);
        }

        private void OnInputFieldValueChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            var valid = float.TryParse(value, out var floatValue);

            if (!valid)
            {
                _inputField.text = FormatFloat(_slider.value);
                return;
            }

            if (floatValue < _slider.minValue) _inputField.text = FormatFloat(_slider.minValue);
            if (floatValue > _slider.maxValue) _inputField.text = FormatFloat(_slider.maxValue);
        }

        private void OnInputFieldEndEdit(string value)
        {
            var valid = float.TryParse(value, out var floatValue);

            if (valid)
            {
                _slider.value = (float)System.Math.Round(floatValue, 2);
                _onValueChanged.Invoke(floatValue);
            }
            else
            {
                _inputField.text = FormatFloat(_slider.value);
            }
        }

        private string FormatFloat(float value)
        {
            return System.Math.Round(value, 2).ToString();
        }
    }
}