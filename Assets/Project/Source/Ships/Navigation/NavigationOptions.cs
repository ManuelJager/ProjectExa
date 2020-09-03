using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public enum NavigationType
    {
        Simple,
        PhysicsBased
    }

    public class NavigationOptions : MonoBehaviour
    {
        [Header("Options")]
        public bool continouslyApplySettings;
        public NavigationType navigationType;

        [Header("PID-Quaternion-parameters")]
        public float qProportionalBase;
        public float qIntegral;
        public float qDerivitive;

        [Header("PD-Position-parameters")]
        public float pProportional;
        public float pDerivitive;
        public float maxVel;

        public INavigation GetNavigation(Ship ship, Blueprint blueprint)
        {
            var template = blueprint.Blocks.Controller.BlueprintBlock.Template as ControllerTemplate;
            var controllerValues = template.controllerTemplatePartial.Convert();

            switch (navigationType)
            {
                case NavigationType.Simple:
                    return new SimpleNavigation(ship, this, controllerValues.directionalForce);

                case NavigationType.PhysicsBased:
                    return new PhysicsNavigation(ship, this, controllerValues.directionalForce);

                default:
                    return null;
            }
        }
    }
}