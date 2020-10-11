using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    [CreateAssetMenu(fileName = "BlueprintType", menuName = "Grids/Blueprints/BlueprintType")]
    public class BlueprintType : ScriptableObject, ITooltipPresenter
    {
        public BlueprintTypeGuid typeGuid;
        public string displayName;
        public Vector2Int maxSize;
        public BlockCategory disallowedBlockCategories;

        private Tooltip tooltipResult;

        public bool IsMothership
        {
            get => typeGuid == BlueprintTypeGuid.mothership;
        }

        private void OnEnable()
        {
            tooltipResult = new Tooltip(GetTooltipGroup);
        }

        public Tooltip GetTooltip()
        {
            return tooltipResult;
        }

        private TooltipGroup GetTooltipGroup() => new TooltipGroup(new ITooltipComponent[]
        {
            new LabeledValue<string>("Max size", $"{maxSize.x}x{maxSize.y}")
        });
    }
}