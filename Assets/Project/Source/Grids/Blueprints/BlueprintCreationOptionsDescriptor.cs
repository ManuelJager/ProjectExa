using Exa.Generics;
using Exa.UI;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintClassificationSource : IDataSourceProvider
    {
        private class BlueprintTypeComparer : IComparer<BlueprintType>
        {
            public int Compare(BlueprintType x, BlueprintType y)
            {
                return GetSize(x) - GetSize(y);
            }

            private int GetSize(BlueprintType blueprintType)
            {
                return blueprintType.maxSize.x * blueprintType.maxSize.y;
            }
        }

        public IEnumerable<NamedWrapper<object>> GetValues()
        {
            var types = MainManager.Instance.blueprintManager.blueprintTypes.objects;
            // Sort blueprint types by their size
            types.Sort(new BlueprintTypeComparer());
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
                VariableTooltipManager.Instance.blueprintTypeTooltip.ShowTooltip((BlueprintType)value);
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

        [Source(typeof(BlueprintClassificationSource))]
        public BlueprintType Class { get; set; }

        public override BlueprintCreationOptions FromDescriptor()
        {
            return new BlueprintCreationOptions(Name, Class.typeGuid);
        }
    }
}