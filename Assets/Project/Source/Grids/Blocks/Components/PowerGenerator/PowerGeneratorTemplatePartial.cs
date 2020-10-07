using Exa.Data;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerGeneratorTemplatePartial : TemplatePartial<PowerGeneratorData>
    {
        [SerializeField] private Scalar _peakGeneration;

        public override PowerGeneratorData Convert() => new PowerGeneratorData
        {
            powerGeneration = _peakGeneration
        };
    }
}