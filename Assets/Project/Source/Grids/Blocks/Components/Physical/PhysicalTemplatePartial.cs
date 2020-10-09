using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalTemplatePartial : TemplatePartial<PhysicalData>
    {
        [SerializeField] private float maxHull;
        [SerializeField] private float armor;
        [SerializeField] private float mass; // In ton

        public float MaxHull => maxHull;
        public float Armor => armor;
        public float Mass => mass;

        public override PhysicalData Convert() => new PhysicalData
        {
            armor = armor,
            hull = maxHull,
            mass = mass
        };
    }
}