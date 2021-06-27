using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class PhysicalTemplatePartial : TemplatePartial<PhysicalData> {
        [SerializeField] private float maxHull;
        [SerializeField] private float armor;
        [SerializeField] private float mass; // In ton

        public float MaxHull {
            get => maxHull;
        }

        public float Armor {
            get => armor;
        }

        public float Mass {
            get => mass;
        }

        public override PhysicalData ToBaseComponentValues() {
            return new PhysicalData {
                armor = armor,
                hull = maxHull,
                mass = mass
            };
        }
    }
}