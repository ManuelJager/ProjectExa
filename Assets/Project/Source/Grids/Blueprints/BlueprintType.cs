using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    [CreateAssetMenu(fileName = "BlueprintType", menuName = "Grids/Blueprints/BlueprintType")]
    public class BlueprintType : ScriptableObject, ITooltipPresenter
    {
        public string displayName;
        public string typeGuid;
        public Vector2Int maxSize;
        public BlockCategory disallowedBlockCategories;

        private Tooltip _tooltipResult;

        private void OnEnable()
        {
            _tooltipResult = new Tooltip(GetTooltipGroup);
        }

        public Tooltip GetTooltip()
        {
            return _tooltipResult;
        }

        private TooltipGroup GetTooltipGroup() => new TooltipGroup(new ITooltipComponent[]
        {
            new LabeledValue<string>("Max size", $"{maxSize.x}x{maxSize.y}")
        });
    }
}