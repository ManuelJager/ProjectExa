using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI
{
    public class FleetBuilder : MonoBehaviour
    {
        [SerializeField] private FleetBuilderBlueprintViewController _viewController;
        [SerializeField] private FleetBuilderBlueprintTypes _blueprintTypes;

        private void Awake()
        {
            _blueprintTypes.BuildList();
        }
    }
}
