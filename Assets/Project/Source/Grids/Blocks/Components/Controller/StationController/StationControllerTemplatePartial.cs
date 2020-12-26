using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class StationControllerTemplatePartial : TemplatePartial<ShipControllerData>
    {
        [SerializeField] private float powerGeneration;
        [SerializeField] private float powerConsumption;
        [SerializeField] private float powerStorage;

        public override ShipControllerData Convert() => new ShipControllerData
        {
            powerGeneration = powerGeneration,
            powerConsumption = powerConsumption,
            powerStorage = powerStorage
        };
    }
}