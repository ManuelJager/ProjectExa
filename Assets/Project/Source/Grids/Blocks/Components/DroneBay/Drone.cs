using System;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class Drone : MonoBehaviour, IDamageable {
        private DroneBayBehaviour bay;
        private IGridInstance parent;

        private Block target;
        private Action targetRemovedHandler;
        private HealthPool healthPool;

        public BlockContext? BlockContext {
            get => parent?.BlockContext;
        }

        public bool IsQueuedForDestruction {
            get => healthPool.value <= 0f;
        }

        public void Setup(DroneBayBehaviour bay) {
            this.bay = bay;
            parent = bay.Parent;

            transform.SetParent(GS.SpawnLayer.ships);

            healthPool = new HealthPool {
                value = bay.Data.droneHull
            };
        }

        private void SetTarget(Block target) {
            if (targetRemovedHandler != null) {
                this.target.OnRemoved -= targetRemovedHandler;
            }
            
            this.target = target;

            targetRemovedHandler = () => {
                // Set to null
                targetRemovedHandler = null;
                this.target = null;
                
                OnTargetDestroyed();
            };

            target.OnRemoved += targetRemovedHandler;
        }

        private void OnTargetDestroyed() {
        }

        public TakenDamage TakeDamage(Damage damage) {
            if (!healthPool.TakeDamage(damage, 0, out var takenDamage)) {
                
            }
            
            return takenDamage;
        }

    }
}