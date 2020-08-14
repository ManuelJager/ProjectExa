using Exa.Generics;
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
        public List<string> disallowedBlockCategories;

        private Tooltip tooltipResult;

        private void OnEnable()
        {
            tooltipResult = new Tooltip(ComponentFactory);
        }

        public IEnumerable<ITooltipComponent> ComponentFactory() => new ITooltipComponent[]
        {
            new LabeledValue<string>("Max size", $"{maxSize.x}x{maxSize.y}")
        };

        public Tooltip GetTooltip()
        {
            return tooltipResult;
        }
    }
}