using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
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
            ship.blockGrid.CurrentHull -= appliedDamage;

            if (data.hull <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        protected override void OnAdd()
        {
            ship.blockGrid.CurrentHull += data.hull;
        }
    }
}