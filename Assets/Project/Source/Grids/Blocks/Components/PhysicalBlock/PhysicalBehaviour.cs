using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IPhysical
    {
        PhysicalBehaviour PhysicalBehaviour { get; }
    }

    public class PhysicalBehaviour : BlockBehaviour<PhysicalData>
    {
        public void TakeDamage(float damage, ref PhysicalData data)
        {
            var trueDamage = damage - data.armor;

            if (trueDamage < 0f) return;

            // Get the min between the current hull and damage that should be applied
            // This prevents the block from receiving too much damage
            var appliedDamage = Mathf.Min(data.hull, trueDamage);
            data.hull -= appliedDamage;

            // Notify the grid the current amount of hull points is decreased
            ship.state.CurrentHull -= appliedDamage;

            if (data.hull <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        protected override void OnAdd()
        {
            ship.state.CurrentHull += data.hull;
            ship.state.TotalMass += data.mass;
        }

        protected override void OnRemove()
        {
            ship.state.TotalMass -= data.mass;
        }
    }
}