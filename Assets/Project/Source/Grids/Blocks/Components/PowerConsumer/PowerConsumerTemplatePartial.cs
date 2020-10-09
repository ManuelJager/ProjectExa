using Exa.Data;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerConsumerTemplatePartial : TemplatePartial<PowerConsumerData>
    {
        [SerializeField] private Scalar powerConsumption; // In MW

        public override PowerConsumerData Convert() => new PowerConsumerData
        {
            powerConsumption = powerConsumption
        };
    }
}