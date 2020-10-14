using Exa.Ships;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI
{
    public class FleetBuilder : MonoBehaviour
    {
        [SerializeField] private FleetView fleetView;
        [SerializeField] private FleetBlueprintViewController viewController;
        [SerializeField] private FleetBuilderBlueprintTypes blueprintTypes;

        public Fleet Fleet => fleetView.Export();

        public void Init()
        {
            blueprintTypes.BuildList(viewController.CreateTab);
            viewController.Init(fleetView.Toggle, fleetView.Remove, Systems.Blueprints.observableUserBlueprints);
            fleetView.Create(5, viewController.GetView);
        }
    }
}
