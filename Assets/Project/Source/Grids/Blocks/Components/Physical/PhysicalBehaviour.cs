using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IPhysical : IBehaviourMarker<PhysicalData>
    { }

    public class PhysicalBehaviour : BlockBehaviour<PhysicalData>
    {
        // TODO: replace damage source by an actual type
        /// <summary>
        /// Takes a given amount of damage
        /// </summary>
        /// <param name="damageSource">Damage source metadata</param>
        /// <param name="damage">The damage to take</param>
        /// <returns></returns>
        public DamageInstanceData AbsorbDamage(object damageSource, float damage) {
            if (Parent.Configuration.Invulnerable) {
                return new DamageInstanceData {
                    absorbedDamage = damage,
                    appliedDamage = 0
                };
            }

            var appliedDamage = Mathf.Min(data.hull, ComputeDamage(damage));
            var instanceData = new DamageInstanceData {
                absorbedDamage = Mathf.Min(data.hull, damage),
                appliedDamage = appliedDamage
            };


            if (GridInstance) {
                GridInstance.Totals.Hull -= appliedDamage;
                GameSystems.PopupManager.CreateOrUpdateDamagePopup(transform.position, damageSource, appliedDamage);
            }

            data.hull -= appliedDamage;
            if (data.hull <= 0) {
                gameObject.SetActive(false);
            }

            return instanceData;
        }

        public float ComputeDamage(float damage) {
            return Mathf.Max(damage - data.armor, 0f);
        }

        protected override void OnAdd() {
            if (Parent.Rigidbody2D.bodyType == RigidbodyType2D.Dynamic) {
                block.Collider.SetMass(data.mass);
            }
        }

        protected override void OnRemove() {
        }
    }

    public struct DamageInstanceData
    {
        public float absorbedDamage;
        public float appliedDamage;
    }
}