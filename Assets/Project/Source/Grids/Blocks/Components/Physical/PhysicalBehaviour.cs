using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IPhysical : IBehaviourMarker<PhysicalData>
    { }

    public class PhysicalBehaviour : BlockBehaviour<PhysicalData>
    {
        public void TakeDamage(float damage) {
            var trueDamage = damage - data.armor;

            if (trueDamage < 0f) return;

            // Get the min between the current hull and damage that should be applied
            // This prevents the block from receiving too much damage
            var appliedDamage = Mathf.Min(data.hull, trueDamage);
            data.hull -= appliedDamage;

            if (data.hull <= 0) {
                gameObject.SetActive(false);
            }
        }

        protected override void OnAdd() {
            var localPos = block.anchoredBlueprintBlock.GetLocalPosition();
            BlockGrid.CentreOfMass.Add(localPos, data.mass);
        }

        protected override void OnRemove() {
            var localPos = block.anchoredBlueprintBlock.GetLocalPosition();
            BlockGrid.CentreOfMass.Remove(localPos);
        }
    }
}