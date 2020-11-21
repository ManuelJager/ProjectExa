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
            var computedDamage = ComputeDamage(damage);
            if (computedDamage < 0f) {
                eventData.absorbedDamage = damage;
            }

            var absorbedDamage = Mathf.Min(data.hull, damage);
            if (Ship)
                Ship.Totals.Hull -= absorbedDamage;

            eventData.absorbedDamage = absorbedDamage;
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

    public struct DamageEventData
    {
        public float absorbedDamage;
    }
}