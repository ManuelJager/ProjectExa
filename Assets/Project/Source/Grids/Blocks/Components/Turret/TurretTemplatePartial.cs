using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class TurretTemplatePartial : TemplatePartial<TurretData>
    {
        [Tooltip("In degrees rotation per second")]
        public float turningRate;

        [Tooltip("In second interval between shots")]
        public float firingRate;

        public float damage;

        public override TurretData ToBaseComponentValues() => new TurretData {
            turningRate = turningRate,
            firingRate = firingRate,
            damage = damage
        };
    }
}