﻿using System;
using Exa.Gameplay;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class PhysicalBehaviour : BlockBehaviour<PhysicalData>, IDamageable, IRepairable {
        [Header("State")]
        [SerializeField] private HealthPool hull;
        public bool IsQueuedForDestruction { get; private set; }
        
        public bool IsRepaired { get; }

        public BlockContext? BlockContext {
            get => Parent?.BlockContext;
        }

        public event Action<float> OnDamage;
        
        // Pass event handling to block
        public event Action OnRemoved {
            add => block.OnRemoved += value;
            remove => block.OnRemoved -= value;
        }

        // TODO: replace damage source by an actual type
        /// <summary>
        ///     Takes a given amount of damage
        /// </summary>
        /// <param name="damage">The damage to take</param>
        /// <returns></returns>
        public TakenDamage TakeDamage(Damage damage) {
            if (IsQueuedForDestruction) {
            #if ENABLE_BLOCK_LOGS
                block.Logs.Add("Function: TakeDamage, Returning because block is queued for destruction");
            #endif

                return new TakenDamage();
            }

            if (!Parent.Configuration.Invulnerable) {
                try {
                    var noHealth = !hull.TakeDamage(damage, data.armor, out var takenDamage);
                    var appliedDamage = takenDamage.appliedDamage;

                    if (GridInstance) {
                        GridInstance.BlockGrid.GetTotals().Hull -= appliedDamage;
                        GS.PopupManager.CreateOrUpdateDamagePopup(transform.position, damage.source, appliedDamage);
                    }

                    if (appliedDamage != 0f) {
                        OnDamage?.Invoke(appliedDamage);
                    }

                    if (noHealth) {
                        IsQueuedForDestruction = true;
                        block.DestroyBlock();
                    }

                    return takenDamage;
                } catch (Exception e) {
                    Debug.LogException(e);
                }
            }

            return new TakenDamage {
                absorbedDamage = damage.value,
                appliedDamage = 0
            };
        }

        public void Repair() {
            hull.value = data.hull;
        }

        public void Repair(float repairedHull) {
            hull.value = Mathf.Min(data.hull, hull.value + repairedHull);
        }

        protected override void OnBlockDataReceived(PhysicalData oldValues, PhysicalData newValues) {
            Parent.Rigidbody2D.mass += newValues.mass - oldValues.mass;
        }

        protected override void OnAdd() {
            Repair();
            IsQueuedForDestruction = false;
        }

        protected override void OnRemove() {
            OnDamage = null;
        }
    }
}