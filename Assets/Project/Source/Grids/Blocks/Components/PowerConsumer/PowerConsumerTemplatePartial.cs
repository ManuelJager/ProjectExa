using Exa.Data;
using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerConsumerTemplatePartial : TemplatePartial<PowerConsumerData>
    {
        [SerializeField] private Scalar _powerConsumption; // In MW

        public override PowerConsumerData Convert() => new PowerConsumerData
        {
            powerConsumption = _powerConsumption
        };
    }
}