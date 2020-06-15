using Exa.Generics;
using Exa.UI.Controls;
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

        public ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new ValueContext { name = "Max size", value = $"{maxSize.x}x{maxSize.y}"}
            };
        }
    }
}