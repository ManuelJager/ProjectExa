using Exa.Generics;
using Exa.UI.Controls;
using Exa.UI.SharedViews;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public class TooltipGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject propertyPrefab;
        [SerializeField] private GameObject titlePrefab;
        [SerializeField] private GameObject spacerPrefab;

        public void GenerateTooltip<T>(T value, Transform parent)
            where T : ITooltipPresenter
        {
            var result = value.GetTooltip();
            GenerateTooltip(result, parent);
        }

        public void GenerateTooltip(Tooltip tooltip, Transform parent)
        {
            foreach (var property in tooltip.GetComponents())
            {
                var tooltipComponentBundle = property.InstantiateComponentView(parent);
                tooltipComponentBundle.componentView.transform.SetParent(parent);
            }
        }

        public TooltipComponentBundle GenerateTooltipProperty<T>(LabeledValue<T> labeledValue, Transform parent)
        {
            var propertyObject = Instantiate(propertyPrefab, parent);
            var propertyView = propertyObject.GetComponent<PropertyView>();
            propertyView.Reflect(labeledValue);
            //Action<object> update = (obj) => propertyView.Reflect((LabeledValue<T>)obj);
            //var componentBinder = new TooltipComponentBinder(update);

            return new TooltipComponentBundle
            {
                componentView = propertyObject,
                //componentBinder = componentBinder
            };
        }

        public TooltipComponentBundle GenerateTooltipTitle(TooltipTitle value, Transform parent)
        {
            var titleObject = Instantiate(titlePrefab, parent);
            var titleView = titleObject.GetComponent<TitleView>();
            titleView.Reflect(value);
            //Action<object> update = (obj) => titleView.Reflect((TooltipTitle)obj);
            //var componentBinder = new TooltipComponentBinder(update);

            return new TooltipComponentBundle
            {
                componentView = titleObject,
                //componentBinder = componentBinder
            };
        }

        public TooltipComponentBundle GenerateTooltipSpacer(TooltipSpacer value, Transform parent)
        {
            var spacerObject = Instantiate(spacerPrefab, parent);

            return new TooltipComponentBundle
            {
                componentView = spacerObject,
                //componentBinder = new TooltipComponentBinder()
            };
        }
    }
}