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

        [Header("PID-Quaternion-parameters")] 
        public float qProportionalBase;
        public float qIntegral;
        public float qDerivative;

        [Header("PD-Position-parameters")] 
        public float pProportional;
        public float pDerivative;
        public float maxVel;

        public INavigation GetNavigation(GridInstance gridInstance, Blueprint blueprint) {
            var template = blueprint.Blocks.Controller.BlueprintBlock.Template as ShipControllerTemplate;
            var controllerValues = template.shipControllerTemplatePartial.Convert();

            switch (navigationType) {
                case NavigationType.Simple:
                    return new SimpleNavigation(gridInstance, this, controllerValues.thrustModifier);

                case NavigationType.Directional:
                    return new DirectionalNavigation(gridInstance, this, controllerValues.thrustModifier);

                default:
                    throw new ArgumentOutOfRangeException(nameof(navigationType));
            }
        }
    }
}