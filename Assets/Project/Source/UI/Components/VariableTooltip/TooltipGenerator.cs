using Exa.Generics;
using Exa.UI.Controls;
using Exa.UI.SharedViews;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public class TooltipGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject propertyPrefab;
        [SerializeField] private GameObject titlePrefab;
        [SerializeField] private GameObject spacerPrefab;

        public IEnumerable<GameObject> GenerateTooltip(TooltipResult result, Transform parent)
        {
            var collection = new List<GameObject>();
            foreach (var property in result.GetComponents())
            {
                var tooltipProperty = property.InstantiateComponentView(parent);
                tooltipProperty.transform.SetParent(parent);
                collection.Add(tooltipProperty);
            }
            return collection;
        }

        public GameObject GenerateTooltipProperty<T>(NamedValue<T> value, Transform parent)
        {
            var propertyObject = Instantiate(propertyPrefab, parent);
            propertyObject.GetComponent<PropertyView>().Reflect(value);
            return propertyObject;
        }

        public GameObject GenerateTooltipTitle(TooltipTitle value, Transform parent)
        {
            var titleObject = Instantiate(titlePrefab, parent);
            titleObject.GetComponent<TitleView>().Reflect(value);
            return titleObject;
        }

        public GameObject GenerateTooltipSpacer(TooltipSpacer value, Transform parent)
        {
            var spacerObject = Instantiate(spacerPrefab, parent);
            return spacerObject;
        }
    }
}