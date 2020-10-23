using Exa.Bindings;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Validation;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0649

namespace Exa.UI
{
    public class FleetBuilder : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private FleetView fleetView;
        [SerializeField] private FleetBlueprintViewController viewController;
        [SerializeField] private FleetBuilderBlueprintTypes blueprintTypes;

        [Header("Events")] 
        public UnityEvent<ValidationResult> fleetValidation = new UnityEvent<ValidationResult>();

        private FleetValidator fleetValidator;

        public Fleet Fleet => fleetView.Fleet;

        public void Init(IObservableEnumerable<BlueprintContainer> source) {
            blueprintTypes.BuildList(viewController.CreateTab);
            viewController.Init(fleetView.Toggle, fleetView.Remove, source);

            // NOTE: placeholder value, should be updated later
            var unityCapacity = 5;
            fleetView.Create(unityCapacity, viewController.GetView);
            fleetValidator = new FleetValidator();

            Validate();
        }

        public void Validate() {
            var validationResult = fleetValidator.Validate(Fleet);
            fleetValidation.Invoke(validationResult);
        }
    }
}