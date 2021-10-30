using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks {
    public interface IDamageable {
        TakenDamage TakeDamage(Damage damage);
        BlockContext? BlockContext { get; }
        bool IsQueuedForDestruction { get; }
    }
    
    public struct Damage {
        public float value;
        public object source;
    }
   
    public struct TakenDamage {
        public float absorbedDamage;
        public float appliedDamage;
    }

    public static class DamageableExtensions {
        /// <summary>
        /// Get whether or not this damageable should be targeted
        /// </summary>
        public static bool PassesDamageMask(this IDamageable damageable, BlockContext damageMask) {
            if (damageable.IsQueuedForDestruction) {
                return false;
            }

            if (damageable.BlockContext.GetHasValue(out var value)) {
                return value.HasAnyValue(damageMask);
            }
            
            Debug.LogError($"Damageable {damageable} has no parent");
            return false;
        }
    }
}