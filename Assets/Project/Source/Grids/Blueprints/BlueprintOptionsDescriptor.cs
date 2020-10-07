using Exa.Generics;
using Exa.UI;
using Exa.UI.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintOptionsDescriptor : ModelDescriptor<BlueprintOptions>
    {
        private string _blueprintName;

        private BlueprintType _blueprintClass;

        public override BlueprintOptions FromDescriptor()
        {
            return new BlueprintOptions(_blueprintName, _blueprintClass.typeGuid);
        }

        public override void GenerateView(Transform container)
        {
            Systems.Ui.controlFactory.CreateInputField(container, "Name", SetBlueprintName);
            Systems.Ui.controlFactory.CreateDropdown(container, "Class", GetPossibleBlueprintClasses(), SetBlueprintClass, OnOptionCreation);
        }

        private void SetBlueprintName(string blueprintName) => this._blueprintName = blueprintName;
        private void SetBlueprintClass(object blueprintClass) => this._blueprintClass = blueprintClass as BlueprintType;

        private IEnumerable<LabeledValue<object>> GetPossibleBlueprintClasses()
        {
            var types = Systems.Blueprints.blueprintTypes.objects;
            foreach (var type in types)
            {
                yield return new LabeledValue<object>
                {
                    Label = type.displayName,
                    Value = type
                };
            }
        }

        private void OnOptionCreation(object value, DropdownTab tab)
        {
            var hoverable = tab.gameObject.AddComponent<Hoverable>();
            hoverable.onPointerEnter.AddListener(() =>
            {
                Systems.Ui.tooltips.blueprintTypeTooltip.Show(value as BlueprintType);
            });
            hoverable.onPointerExit.AddListener(() =>
            {
                Systems.Ui.tooltips.blueprintTypeTooltip.Hide();
            });
        }
    }
}