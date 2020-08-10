using Exa.Generics;
using Exa.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintClassSource : IDataSourceProvider
    {
        public IEnumerable<NamedWrapper<object>> GetValues()
        {
            var types = Systems.Blueprints.blueprintTypes.objects;
            foreach (var type in types)
            {
                yield return new NamedWrapper<object>
                {
                    Name = type.displayName,
                    Value = type
                };
            }
        }

        public void OnOptionCreation(object value, GameObject viewObject)
        {
            var hoverable = viewObject.AddComponent<Hoverable>();
            hoverable.onPointerEnter.AddListener(() =>
            {
                Systems.UI.variableTooltipManager.blueprintTypeTooltip.ShowTooltip((BlueprintType)value);
            });
            hoverable.onPointerExit.AddListener(() =>
            {
                Systems.UI.variableTooltipManager.blueprintTypeTooltip.HideTooltip();
            });
        }
    }

    public class BlueprintOptionsDescriptor : ModelDescriptor<BlueprintOptions>
    {
        public string Name { get; set; }

        [Source(typeof(BlueprintClassSource))]
        public BlueprintType Class { get; set; }

        public override BlueprintOptions FromDescriptor()
        {
            return new BlueprintOptions(Name, Class.typeGuid);
        }
    }
}