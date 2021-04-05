using System;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public enum NavigationType
    {
        Simple,
        Directional,
        NewDirectional
    }

    public class NavigationOptions : MonoBehaviour
    {
        [Header("Options")] 
        public bool continuouslyApplySettings;
        public NavigationType navigationType;

        public INavigation GetNavigation(GridInstance gridInstance, Blueprint blueprint) {
            var template = blueprint.Blocks.Controller.BlueprintBlock.Template as ShipControllerTemplate;
            var controllerValues = template.shipControllerTemplatePartial.ToBaseComponentValues();

            return navigationType switch {
                NavigationType.Simple => new SimpleNavigation(gridInstance, this, controllerValues.thrustModifier),
                NavigationType.Directional => new DirectionalNavigation(gridInstance, this, controllerValues.thrustModifier),
                _ => throw new ArgumentOutOfRangeException(nameof(navigationType))
            };
        }
    }
}