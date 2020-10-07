using Exa.Generics;
using Exa.UI.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Controls
{
    public class ControlFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _dropdownPrefab;
        [SerializeField] private GameObject _inputFieldPrefab;
        [SerializeField] private GameObject _radioPrefab;
        [SerializeField] private GameObject _sliderPrefab;

        public DropdownControl CreateDropdown(Transform container, string label, IEnumerable<LabeledValue<object>> possibleValues, Action<object> setter, Action<object, DropdownTab> onTabCreated = null)
        {
            var dropdown = CreateControl<DropdownControl, object>(container, _dropdownPrefab, label, setter);
            dropdown.CreateTabs(possibleValues, onTabCreated);
            return dropdown;
        }

        public InputFieldControl CreateInputField(Transform container, string label, Action<string> setter)
        {
            var inputField = CreateControl<InputFieldControl, string>(container, _inputFieldPrefab, label, setter);
            inputField.Setup($"input {label.ToLower()}...");
            return inputField;
        }

        public RadioControl CreateRadio(Transform container, string label, Action<bool> setter)
        {
            return CreateControl<RadioControl, bool>(container, _radioPrefab, label, setter);
        }

        public SliderControl CreateSlider(Transform container, string label, Action<float> setter, MinMax<float>? minMax = null)
        {
            var slider = CreateControl<SliderControl, float>(container, _sliderPrefab, label, setter);
            slider.SetMinMax(minMax ?? MinMax<float>.ZeroOne);
            return slider;
        }

        private T CreateControl<T, TS>(Transform container, GameObject prefab, string label, Action<TS> setter)
            where T : InputControl<TS>
        {
            var control = Instantiate(prefab, container).GetComponent<T>();
            control.SetLabelText(label);
            control.OnValueChange.AddListener((obj) => setter(obj));
            return control;
        }
    }
}