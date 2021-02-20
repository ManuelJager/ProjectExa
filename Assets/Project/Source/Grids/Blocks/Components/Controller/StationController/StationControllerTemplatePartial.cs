using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class StationControllerTemplatePartial : TemplatePartial<StationControllerData>
    {
        [SerializeField] private float powerGeneration;
        [SerializeField] private float powerConsumption;
        [SerializeField] private float powerStorage;
        [SerializeField] private float turningRate;

        public override StationControllerData ToBaseComponentValues() => new StationControllerData {
            powerGeneration = powerGeneration,
            powerConsumption = powerConsumption,
            powerStorage = powerStorage,
            turningRate = turningRate
        };
    }
}