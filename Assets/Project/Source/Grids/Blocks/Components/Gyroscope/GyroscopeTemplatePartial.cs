using Exa.Data;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class GyroscopeTemplatePartial : TemplatePartial<GyroscopeData>
    {
        [SerializeField] private Scalar turningRate;

        public override GyroscopeData Convert() => new GyroscopeData {
            turningRate = turningRate
        };
    }
}