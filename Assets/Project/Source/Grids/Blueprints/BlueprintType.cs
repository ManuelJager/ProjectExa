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

        private TooltipResult tooltipResult;

        private void OnEnable()
        {
            tooltipResult = new TooltipResult(ComponentFactory);
        }

        public ITooltipComponent[] ComponentFactory()
        {
            return new ITooltipComponent[]
            {
                new NamedValue<string>("Max size", $"{maxSize.x}x{maxSize.y}")
            };
        }

        public TooltipResult GetComponents()
        {
            return tooltipResult;
        }
    }
}