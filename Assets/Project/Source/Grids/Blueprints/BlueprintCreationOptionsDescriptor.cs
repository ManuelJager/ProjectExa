using Exa.Generics;
using Exa.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintClassificationSource : IDataSourceProvider
    {
        public IEnumerable<NamedWrapper<object>> GetValues()
        {
            var types = MainManager.Instance.blueprintManager.blueprintTypes.objects;
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
                MainManager.Instance.tooltipManager.blueprintTypeTooltip.ShowTooltip((BlueprintType)value);
            });
            hoverable.onPointerExit.AddListener(() =>
            {
                MainManager.Instance.tooltipManager.blueprintTypeTooltip.HideTooltip();
            });
        }
    }

    public class BlueprintCreationOptionsDescriptor : ModelDescriptor<BlueprintCreationOptions>
    {
        public string Name { get; set; }

        [Source(typeof(BlueprintClassificationSource))]
        public BlueprintType Class { get; set; }

        public override BlueprintCreationOptions FromDescriptor()
        {
            return new BlueprintCreationOptions(Name, Class.typeGuid);
        }
    }
}