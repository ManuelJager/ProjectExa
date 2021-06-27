using System;
using Exa.Data;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class ShipControllerTemplatePartial : TemplatePartial<ShipControllerData> {
        [SerializeField] private Scalar powerGenerationModifier;
        [SerializeField] private Scalar turningPowerModifier;
        [SerializeField] private Scalar thrustModifier;

        public override ShipControllerData ToBaseComponentValues() {
            return new ShipControllerData {
                powerGenerationModifier = powerGenerationModifier,
                turningPowerModifier = turningPowerModifier,
                thrustModifier = thrustModifier
            };
        }
    }
}