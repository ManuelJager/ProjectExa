using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI
{
    public class FleetBuilder : MonoBehaviour
    {
        [SerializeField] private FleetBuilderBlueprintViewController viewController;
        [SerializeField] private FleetBuilderBlueprintTypes blueprintTypes;

        private void Awake()
        {
            blueprintTypes.BuildList();
        }
    }
}
