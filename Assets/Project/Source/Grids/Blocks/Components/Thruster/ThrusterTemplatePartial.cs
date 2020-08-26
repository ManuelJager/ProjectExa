using Exa.Data;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ThrusterTemplatePartial : TemplatePartial<ThrusterData>
    {
        [SerializeField] private Percentage thrust; // In % over the base 

        public override ThrusterData Convert() => new ThrusterData
        {
            thrust = thrust
        };
    }
}