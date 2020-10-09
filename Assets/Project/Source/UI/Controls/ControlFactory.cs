using Exa.Generics;
using System;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI.Controls
{
    public class ControlFactory : MonoBehaviour
    {
        [SerializeField] private GameObject dropdownPrefab;
        [SerializeField] private GameObject inputFieldPrefab;
        [SerializeField] private GameObject radioPrefab;
        [SerializeField] private GameObject sliderPrefab;

        public DropdownControl CreateDropdown(Transform container, string label, IEnumerable<LabeledValue<object>> possibleValues, Action<object> setter, Action<object, DropdownTab> onTabCreated = null)
        {
            var dropdown = CreateControl<DropdownControl, object>(container, dropdownPrefab, label, setter);
            dropdown.CreateTabs(possibleValues, onTabCreated);
            return dropdown;
        }

        public InputFieldControl CreateInputField(Transform container, string label, Action<string> setter)
        {
            var inputField = CreateControl<InputFieldControl, string>(container, inputFieldPrefab, label, setter);
            inputField.Setup($"input {label.ToLower()}...");
            return inputField;
        }

        public RadioControl CreateRadio(Transform container, string label, Action<bool> setter)
        {
            return CreateControl<RadioControl, bool>(container, radioPrefab, label, setter);
        }

        public SliderControl CreateSlider(Transform container, string label, Action<float> setter, MinMax<float>? minMax = null)
        {
            var slider = CreateControl<SliderControl, float>(container, sliderPrefab, label, setter);
            slider.SetMinMax(minMax ?? MinMax<float>.ZeroOne);
            return slider;
        }

        private T CreateControl<T, S>(Transform container, GameObject prefab, string label, Action<S> setter)
            where T : InputControl<S>
        {
            var control = Instantiate(prefab, container).GetComponent<T>();
            control.SetLabelText(label);
            control.OnValueChange.AddListener(obj => setter(obj));
            return control;
        }
    }
}