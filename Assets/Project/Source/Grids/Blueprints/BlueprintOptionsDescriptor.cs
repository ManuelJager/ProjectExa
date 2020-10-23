using Exa.Generics;
using Exa.UI;
using Exa.UI.Controls;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintOptionsDescriptor : ModelDescriptor<BlueprintOptions>
    {
        private string blueprintName;

        private BlueprintType blueprintType;

        public override BlueprintOptions FromDescriptor()
        {
            return new BlueprintOptions(blueprintName, blueprintType.typeGuid);
        }

        public override void GenerateView(Transform container)
        {
            Systems.UI.controlFactory.CreateInputField(container, "Name", SetBlueprintName);
            Systems.UI.controlFactory.CreateDropdown(container, "Class", Systems.Blueprints.blueprintTypes, SetBlueprintType, OnOptionCreation);
        }

        private void SetBlueprintName(string blueprintName) => this.blueprintName = blueprintName;
        private void SetBlueprintType(BlueprintType blueprintType) => this.blueprintType = blueprintType;

        private void OnOptionCreation(BlueprintType value, DropdownTab tab)
        {
            var hoverable = tab.gameObject.AddComponent<Hoverable>();
            hoverable.onPointerEnter.AddListener(() => Systems.UI.tooltips.blueprintTypeTooltip.Show(value));
            hoverable.onPointerExit.AddListener(() => Systems.UI.tooltips.blueprintTypeTooltip.Hide());
        }
    }
}