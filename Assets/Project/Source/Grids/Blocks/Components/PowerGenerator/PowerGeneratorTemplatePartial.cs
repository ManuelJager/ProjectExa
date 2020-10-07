using Exa.Data;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerGeneratorTemplatePartial : TemplatePartial<PowerGeneratorData>
    {
        [SerializeField] private Scalar peakGeneration;

        public override PowerGeneratorData Convert() => new PowerGeneratorData
        {
            powerGeneration = peakGeneration
        };
    }
}