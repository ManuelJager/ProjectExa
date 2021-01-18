using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class AutocannonTemplatePartial : TemplatePartial<AutocannonData>
    {
        [SerializeField] private float turningRate; // Degrees per second
        [SerializeField] private float firingRate; // Shot interval
        [SerializeField] private float turretArc;
        [SerializeField] private float turretRadius;
        [SerializeField] private float damage; // Damage per shot
        [SerializeField] private CycleMode cycleMode; // Wether guns are cycled or fired simoultaniously

        public override AutocannonData ToBaseComponentValues() => new AutocannonData {
            turningRate = turningRate,
            firingRate = firingRate,
            turretArc = turretArc,
            turretRadius = turretRadius,
            damage = damage,
            cycleMode = cycleMode
        };
    }
}