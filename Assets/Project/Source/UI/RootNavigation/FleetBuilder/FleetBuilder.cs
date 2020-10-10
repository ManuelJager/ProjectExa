using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI
{
    public class FleetBuilder : MonoBehaviour
    {
        [SerializeField] private FleetBuilderBlueprintViewController viewController;
        [SerializeField] private FleetBuilderBlueprintTypes blueprintTypes;

        public void Init()
        {
            blueprintTypes.BuildList(viewController.CreateTab);
            viewController.Source = Systems.Blueprints.observableUserBlueprints;
        }
    }
}
