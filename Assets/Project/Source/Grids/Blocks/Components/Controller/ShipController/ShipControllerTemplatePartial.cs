using System;
using Exa.Data;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ShipControllerTemplatePartial : TemplatePartial<ShipControllerData>
    {
        [SerializeField] private float powerGeneration;
        [SerializeField] private float powerConsumption;
        [SerializeField] private float powerStorage;
        [SerializeField] private float turningRate;
        [SerializeField] private Scalar thrustModifier;

        public override ShipControllerData Convert() => new ShipControllerData {
            powerGeneration = powerGeneration,
            powerConsumption = powerConsumption,
            powerStorage = powerStorage,
            turningRate = turningRate,
            thrustModifier = thrustModifier
        };
    }
}