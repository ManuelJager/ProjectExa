using System;
using UnityEngine;

namespace Exa.Grids.Blocks {
    [Serializable]
    public struct HealthPool {
        public float health;

        public bool TakeDamage(Damage damage, float armor, out ReceivedDamage receivedDamage) {
            health -= damage.value;

            receivedDamage = new ReceivedDamage {
                absorbedDamage = Mathf.Min(health, damage.value),
                appliedDamage = Mathf.Min(health, ComputeDamage(damage.value, armor))
            };

            return health > 0;
        }

        private float ComputeDamage(float damage, float armor) {
            return Mathf.Max(damage - armor, 0f);
        }
    }
}