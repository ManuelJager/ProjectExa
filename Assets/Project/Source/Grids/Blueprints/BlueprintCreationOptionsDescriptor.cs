using Exa.Generics;
using Exa.UI;
using Exa.UI.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintClassificationSource : IDataSourceProvider
    {
        public IEnumerable<ValueContext> GetValues()
        {
            var types = GameManager.Instance.blueprintManager.blueprintTypes.typesById.Values;
            foreach (var type in types)
            {
                yield return new ValueContext
                {
                    name = type.displayName,
                    value = type.typeGuid
                };
            }
        }
    }

    public class BlueprintClassificationOptionCreation : IOptionCreationListener
    {
        public void OnOptionCreation(string value, GameObject viewObject)
        {
            var hoverable = viewObject.AddComponent<Hoverable>();
            // Get the blueprint type for the tooltip
            var blueprintType = GameManager.Instance.blueprintManager.blueprintTypes.typesById[value];
            hoverable.onPointerEnter.AddListener(() => // Error here
            {
                VariableTooltipManager.Instance.blueprintTypeTooltip.ShowTooltip(blueprintType);
            });
            hoverable.onPointerExit.AddListener(() =>
            {
                VariableTooltipManager.Instance.blueprintTypeTooltip.HideTooltip();
            });
        }
    }

    public class BlueprintCreationOptionsDescriptor : ModelDescriptor<BlueprintCreationOptions>
    {
        public string Name { get; set; }

        [Source(typeof(BlueprintClassificationSource), typeof(BlueprintClassificationOptionCreation))]
        public string Class { get; set; }

        public override BlueprintCreationOptions FromDescriptor()
        {
            return new BlueprintCreationOptions(Name, Class);
        }
    }
}