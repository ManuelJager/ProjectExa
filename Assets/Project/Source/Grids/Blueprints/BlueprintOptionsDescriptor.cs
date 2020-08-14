using Exa.Generics;
using Exa.UI;
using Exa.UI.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintOptionsDescriptor : ModelDescriptor<BlueprintOptions>
    {
        private string blueprintName;

        private BlueprintType blueprintClass;

        public override BlueprintOptions FromDescriptor()
        {
            return new BlueprintOptions(blueprintName, blueprintClass.typeGuid);
        }

        public override void GenerateView(Transform container)
        {
            Systems.UI.controlFactory.CreateInputField(container, "Name", SetBlueprintName);
            Systems.UI.controlFactory.CreateDropdown(container, "Class", GetPossibleBlueprintClasses(), SetBlueprintClass, OnOptionCreation);
        }

        private void SetBlueprintName(string blueprintName) => this.blueprintName = blueprintName;
        private void SetBlueprintClass(object blueprintClass) => this.blueprintClass = blueprintClass as BlueprintType;

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
                Systems.UI.tooltips.blueprintTypeTooltip.ShowTooltip(value as BlueprintType);
            });
            hoverable.onPointerExit.AddListener(() =>
            {
                Systems.UI.tooltips.blueprintTypeTooltip.HideTooltip();
            });
        }
    }
}