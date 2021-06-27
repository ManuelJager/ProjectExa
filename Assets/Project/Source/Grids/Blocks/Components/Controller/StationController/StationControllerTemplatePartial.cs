using System;
using Exa.Data;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class StationControllerTemplatePartial : TemplatePartial<StationControllerData> {
        [SerializeField] private Scalar powerGenerationModifier;
        [SerializeField] private Scalar turningPowerModifier;

        public override StationControllerData ToBaseComponentValues() {
            return new StationControllerData {
                powerGenerationModifier = powerGenerationModifier,
                turningPowerModifier = turningPowerModifier
            };
        }
    }
}