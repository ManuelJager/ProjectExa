using Exa.Generics;
using Exa.UI.Controls;
using UnityEngine;

namespace Exa.UI.Components
{
    public class FormGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject inputFieldPrefab;
        [SerializeField] private GameObject dropdownPrefab;
        [SerializeField] private Transform controlsContainer;

        public void GenerateForm<T>(ModelDescriptor<T> modelDescriptor)
        {
            foreach (Transform child in controlsContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (var propertyContext in modelDescriptor.GetProperties())
            {
                switch (propertyContext.controlType)
                {
                    case ControlType.inputField:
                        CreateInputFieldControl(propertyContext, modelDescriptor);
                        break;

                    case ControlType.dropdown:
                        CreateDropdownControl(propertyContext, modelDescriptor);
                        break;

                    default:
                        break;
                }
            }
        }

        public void CreateDropdownControl(PropertyContext propertyContext, ModelDescriptor modelDescriptor)
        {
            var control = Instantiate(dropdownPrefab, controlsContainer).GetComponent<Dropdown>();
            control.onDropdownTabValueSelected.AddListener((value) =>
            {
                propertyContext.propertyInfo.SetValue(modelDescriptor, value);
            });
            control.CreateTabs(propertyContext.propertyInfo.Name, propertyContext.sourceAttribute.DataSourceProvider.GetValues());

            // Set callbacks
            if (propertyContext.sourceAttribute.OptionCreationlistener != null)
            {
                foreach (var item in control.tabByOption)
                {
                    propertyContext.sourceAttribute.OptionCreationlistener.OnOptionCreation(item.Key, item.Value.gameObject);
                }
            }
        }

        public void CreateInputFieldControl(PropertyContext propertyContext, ModelDescriptor modelDescriptor)
        {
            var control = Instantiate(inputFieldPrefab, controlsContainer).GetComponent<NamedInputField>();
            var name = propertyContext.propertyInfo.Name;
            control.Setup(name, $"input {name.ToLower()}...");
            control.inputField.onValueChanged.AddListener((value) =>
            {
                propertyContext.propertyInfo.SetValue(modelDescriptor, value);
            });
        }
    }
}