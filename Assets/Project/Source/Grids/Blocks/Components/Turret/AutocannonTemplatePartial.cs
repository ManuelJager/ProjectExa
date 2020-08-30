using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class AutocannonTemplatePartial : TemplatePartial<AutocannonData>
    {
        public float turningRate;   // Degrees per second
        public float firingRate;    // Shot interval
        public float damage;        // Damage per shot
        public CycleMode cycleMode; // Wether guns are cycled or fired simoultaniously

        public override AutocannonData Convert() => new AutocannonData
        {
            turningRate = turningRate,
            firingRate = firingRate,
            damage = damage,
            cycleMode = cycleMode
        };
    }
}