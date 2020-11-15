using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IPhysical : IBehaviourMarker<PhysicalData>
    { }

    public class PhysicalBehaviour : BlockBehaviour<PhysicalData>
    {
        /// <summary>
        /// Takes a given amount of damage
        /// </summary>
        /// <param name="damage"></param>
        /// <returns>Whether all given damage was absorbed</returns>
        public bool TakeDamage(float damage) {
            var computedDamage = ComputeDamage(damage);
            if (computedDamage < 0f) return true;

            // Get the min between the current hull and damage that should be applied
            // This prevents the block from receiving too much damage
            data.hull -= computedDamage;

            if (data.hull <= 0) {
                gameObject.SetActive(false);
            }

            return data.hull >= 0f;
        }

        public float ComputeDamage(float damage) {
            return damage - data.armor;
        }

        protected override void OnAdd() {
            block.Collider.SetMass(data.mass);
        }

        protected override void OnRemove() {
            
        }

    }

    public struct DamageOperationData
    {
        public bool destroyedBlock;
    }
}