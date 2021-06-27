using System;
using Exa.Data;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class PowerConsumerTemplatePartial : TemplatePartial<PowerConsumerData> {
        [SerializeField] private Scalar powerConsumption; // In MW

        public override PowerConsumerData ToBaseComponentValues() {
            return new PowerConsumerData {
                powerConsumption = powerConsumption
            };
        }
    }
}