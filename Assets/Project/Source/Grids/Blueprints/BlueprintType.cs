using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    [CreateAssetMenu(fileName = "BlueprintType", menuName = "Grids/Blueprints/BlueprintType")]
    public class BlueprintType : ScriptableObject, ITooltipPresenter, ILabeledValue<BlueprintType>
    {
        public BlueprintTypeGuid typeGuid;
        public string displayName;
        public Vector2Int maxSize;
        public BlockCategory disallowedBlockCategories;

        private Tooltip tooltipResult;

        public string Label => displayName;
        public BlueprintType Value => this;

        public bool IsMothership {
            get => typeGuid == BlueprintTypeGuid.mothership;
        }

        private void OnEnable() {
            tooltipResult = new Tooltip(GetTooltipGroup);
        }

        public Tooltip GetTooltip() {
            return tooltipResult;
        }

        private TooltipGroup GetTooltipGroup() => new TooltipGroup(new ITooltipComponent[] {
            new LabeledValue<object>("Max size", $"{maxSize.x}x{maxSize.y}")
        });
    }
}