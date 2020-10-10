using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI
{
    public class FleetBuilder : MonoBehaviour
    {
        [SerializeField] private FleetBuilderBlueprintViewController viewController;
        [SerializeField] private FleetBuilderBlueprintTypes blueprintTypes;

        private void Awake()
        {
            //blueprintTypes.BuildList();
        }
    }
}
