using Exa.Generics;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Controls
{
    public class TooltipGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject propertyPrefab;

        public List<GameObject> GenerateTooltip(ITooltipPresenter tooltipPresenter, Transform parent)
        {
            var collection = new List<GameObject>();
            foreach (var property in tooltipPresenter.GetComponents())
            {
                var tooltipProperty = property.InstantiateComponentView(parent);
                tooltipProperty.transform.SetParent(parent);
                collection.Add(tooltipProperty);
            }
            return collection;
        }

        public GameObject GenerateTooltipProperty<T>(NamedValue<T> valueContext, Transform parent)
        {
            var propertyObject = Instantiate(propertyPrefab, parent);
            var propertyView = propertyObject.GetComponent<PropertyView>();
            propertyView.Reflect(valueContext);
            propertyView.SetKeyOnly(valueContext.Name == "");
            return propertyObject;
        }
    }
}