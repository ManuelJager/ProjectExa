using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public enum NavigationType
    {
        Simple,
        Directional
    }

    public class NavigationOptions : MonoBehaviour
    {
        [Header("Options")] public bool continuouslyApplySettings;
        public NavigationType navigationType;

        [Header("PID-Quaternion-parameters")] public float qProportionalBase;
        public float qIntegral;
        public float qDerivative;

        [Header("PD-Position-parameters")] public float pProportional;
        public float pDerivative;
        public float maxVel;

        public INavigation GetNavigation(Ship ship, Blueprint blueprint) {
            var template = blueprint.Blocks.Controller.BlueprintBlock.Template as ControllerTemplate;
            var controllerValues = template.controllerTemplatePartial.Convert();

            switch (navigationType) {
                case NavigationType.Simple:
                    return new SimpleNavigation(ship, this, controllerValues.thrustModifier);

                case NavigationType.Directional:
                    return new DirectionalNavigation(ship, this, controllerValues.thrustModifier);

                default:
                    return null;
            }
        }
    }
}