using System;
using Exa.Data;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ControllerTemplatePartial : TemplatePartial<ControllerData>
    {
        [SerializeField] private float _powerGeneration;
        [SerializeField] private float _powerConsumption;
        [SerializeField] private float _powerStorage;
        [SerializeField] private float _turningRate;
        [SerializeField] private Scalar _thrustModifier;

        public override ControllerData Convert() => new ControllerData
        {
            powerGeneration = _powerGeneration,
            powerConsumption = _powerConsumption,
            powerStorage = _powerStorage,
            turningRate = _turningRate,
            thrustModifier = _thrustModifier
        };
    }
}