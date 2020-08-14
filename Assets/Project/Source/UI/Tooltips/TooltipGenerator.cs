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

        public TooltipBinder<T> GenerateTooltip<T>(T value, Transform parent)
            where T : ITooltipPresenter
        {
            var result = value.GetTooltip();
            var binders = new List<TooltipComponentBinder>();

            foreach (var property in result.GetComponents())
            {
                var tooltipComponentBundle = property.InstantiateComponentView(parent);
                tooltipComponentBundle.componentView.transform.SetParent(parent);
                binders.Add(tooltipComponentBundle.componentBinder);
            }

            return new TooltipBinder<T>(binders);
        }

        public TooltipComponentBundle GenerateTooltipProperty<T>(Transform parent)
        {
            var propertyObject = Instantiate(propertyPrefab, parent);
            var propertyView = propertyObject.GetComponent<PropertyView>();
            Action<object> update = (obj) => propertyView.Reflect((LabeledValue<T>)obj);
            var componentBinder = new TooltipComponentBinder(update);

            return new TooltipComponentBundle
            {
                componentView = propertyObject,
                componentBinder = componentBinder
            };
        }

        public TooltipComponentBundle GenerateTooltipTitle(TooltipTitle value, Transform parent)
        {
            var titleObject = Instantiate(titlePrefab, parent);
            var titleView = titleObject.GetComponent<TitleView>();
            Action<object> update = (obj) => titleView.Reflect((TooltipTitle)obj);
            var componentBinder = new TooltipComponentBinder(update);

            return new TooltipComponentBundle
            {
                componentView = titleObject,
                componentBinder = componentBinder
            };
        }

        public TooltipComponentBundle GenerateTooltipSpacer(TooltipSpacer value, Transform parent)
        {
            var spacerObject = Instantiate(spacerPrefab, parent);

            return new TooltipComponentBundle
            {
                componentView = spacerObject,
                componentBinder = new TooltipComponentBinder()
            };
        }
    }
}