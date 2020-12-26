using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class GaussCannonTemplatePartial : TemplatePartial<GaussCannonData>
    {
        [SerializeField] private float turningRate;
        [SerializeField] private float firingRate;
        [SerializeField] private float damage;

        public override GaussCannonData Convert() => new GaussCannonData {
            turningRate = turningRate,
            firingRate = firingRate,
            damage = damage
        };
    }
}