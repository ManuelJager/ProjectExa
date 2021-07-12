using System;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class PhysicalBehaviour : BlockBehaviour<PhysicalData>, IDamageable {
        [Header("State")]
        [SerializeField] private HealthPool hull;

        public event Action<float> OnDamage;

        // TODO: replace damage source by an actual type
        /// <summary>
        ///     Takes a given amount of damage
        /// </summary>
        /// <param name="damageSource">Damage source metadata</param>
        /// <param name="damage">The damage to take</param>
        /// <returns></returns>
        public ReceivedDamage AbsorbDamage(Damage damage) {
            if (Parent.Configuration.Invulnerable) {
                return new ReceivedDamage {
                    absorbedDamage = damage.value,
                    appliedDamage = 0
                };
            }

            var isDestroyed = !hull.TakeDamage(damage, data.armor, out var receivedDamage);
            var appliedDamage = receivedDamage.appliedDamage;

            if (GridInstance) {
                GridInstance.BlockGrid.GetTotals().Hull -= appliedDamage;
                GS.PopupManager.CreateOrUpdateDamagePopup(transform.position, damage.source, appliedDamage);
            }

            if (appliedDamage != 0f) {
                OnDamage?.Invoke(appliedDamage);
            }

            if (isDestroyed) {
                block.DestroyBlock();
            }

            return receivedDamage;
        }

        public void Repair() {
            hull.health = data.hull;
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
}