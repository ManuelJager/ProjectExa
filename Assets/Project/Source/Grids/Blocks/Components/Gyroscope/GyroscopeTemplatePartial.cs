using Exa.Data;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class GyroscopeTemplatePartial : TemplatePartial<GyroscopeData>
    {
        [SerializeField] private Scalar _turningRate;

        public override GyroscopeData Convert() => new GyroscopeData
        {
            turningRate = _turningRate
        };
    }
}