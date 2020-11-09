using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IPhysical : IBehaviourMarker<PhysicalData>
    { }

    public class PhysicalBehaviour : BlockBehaviour<PhysicalData>
    {
        [SerializeField, HideInInspector] private BlockCollider blockCollider;

        public BlockCollider BlockCollider {
            get => blockCollider;
            set => blockCollider = value;
        }

        public void TakeDamage(float damage) {
            damage = ComputeDamage(damage);

            if (damage < 0f) return;

            // Get the min between the current hull and damage that should be applied
            // This prevents the block from receiving too much damage
            var appliedDamage = Mathf.Min(data.hull, damage);
            data.hull -= appliedDamage;

            if (data.hull <= 0) {
                gameObject.SetActive(false);
            }
        }

        public float ComputeDamage(float damage) {
            return damage - data.armor;
        }

        protected override void OnAdd() {
            blockCollider.Collider.SetMass(data.mass);
        }

        protected override void OnRemove() {
            
        }
    }
}