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
        public bool AbsorbDamage(float damage, out DamageEventData eventData) {
            eventData = new DamageEventData();

            var appliedDamage = Mathf.Min(data.hull, ComputeDamage(damage));
            eventData.absorbedDamage = damage;
            eventData.appliedDamage = appliedDamage;
            data.hull -= appliedDamage;

            if (Ship) {
                Ship.Totals.Hull -= appliedDamage;
                GameSystems.PopupManager.CreateDamagePopup(transform.position, appliedDamage);
            }

            if (data.hull <= 0) {
                gameObject.SetActive(false);
            }

            return data.hull >= 0f;
        }

        public float ComputeDamage(float damage) {
            return Mathf.Max(damage - data.armor, 1f);
        }

        protected override void OnAdd() {
            block.Collider.SetMass(data.mass);
        }

        protected override void OnRemove() {
        }
    }

    public struct DamageEventData
    {
        public float absorbedDamage;
        public float appliedDamage;
    }
}