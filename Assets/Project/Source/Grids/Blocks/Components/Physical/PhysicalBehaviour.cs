using System;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class PhysicalBehaviour : BlockBehaviour<PhysicalData> {
        public event Action<float> OnDamage;

        // TODO: replace damage source by an actual type
        /// <summary>
        ///     Takes a given amount of damage
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
                GridInstance.BlockGrid.GetTotals().Hull -= appliedDamage;
                GS.PopupManager.CreateOrUpdateDamagePopup(transform.position, damageSource, appliedDamage);
            }

            data.hull -= appliedDamage;

            if (appliedDamage != 0f) {
                OnDamage?.Invoke(appliedDamage);
            }

            if (data.hull <= 0) {
                block.DestroyBlock();
            }

            return instanceData;
        }

        public void Repair() {
            var context = Parent.BlockContext;
            var template = block.BlueprintBlock.Template;

            if (!S.Blocks.Values.TryGetValues(context, template, out data)) {
                throw new Exception($"Cannot set physical data for {block.GetInstanceString()}");
            }
        }

        public float ComputeDamage(float damage) {
            return Mathf.Max(damage - data.armor, 0f);
        }

        protected override void OnAdd() {
            Parent.Rigidbody2D.mass += data.mass;
        }

        protected override void OnRemove() {
            Parent.Rigidbody2D.mass -= data.mass;

            if (OnDamage != null) {
                foreach (var d in OnDamage.GetInvocationList()) {
                    OnDamage -= (Action<float>) d;
                }
            }
        }
    }

    public struct DamageInstanceData {
        public float absorbedDamage;
        public float appliedDamage;
    }
}