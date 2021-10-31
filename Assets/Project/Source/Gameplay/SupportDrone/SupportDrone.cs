using System;
using Cinemachine.Utility;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using UnityEditor;
using UnityEngine;

namespace Exa.Gameplay {
    public enum SupportDroneTaskType {
        Repair,
    }
    
    public class SupportDrone : MonoBehaviour, IDamageable {
        [Header("State")]
        [SerializeField] private DroneBayBehaviour bay;
        [SerializeField] private HealthPool healthPool;
        [SerializeField] private SupportDroneData data;
        private IGridInstance parent;
        private SupportDroneTask task;
        
        private Action targetRemovedHandler;
        private OrchestratorGridClaim claim;

        public BlockContext? BlockContext {
            get => parent?.BlockContext;
        }

        public bool IsQueuedForDestruction {
            get => healthPool.value <= 0f;
        }

        public bool IsBusy {
            get => task != null;
        }

        public void Setup(DroneBayBehaviour bay, Transform nextParent) {
            parent = bay.Parent;

            data = bay.Data.droneData;
            
            healthPool = new HealthPool {
                value = data.hull
            };

            transform.SetParent(nextParent);
            
            parent.SupportDroneOrchestrator.SubmitDrone(this);
        }

        public OrchestratorGridClaim SetTask(SupportDroneTask newTask) {
            task = newTask;

            return task.Type switch {
                SupportDroneTaskType.Repair => OnReceiveRepairTask(task as RepairTask),
                _ => throw new ArgumentException($"type ${task.GetType()} not supported", nameof(task))
            };
        }

        private void Update() {
            if (GS.IsPaused || task == null) {
                return;
            }
            
            MoveTowards(task.GetPosition());
        }

        private void MoveTowards(Vector2 position) {
            var delta = 1f * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, position, delta);

            if ((transform.position.ToVector2() - position).magnitude <= float.Epsilon) {
                Debug.Log("Arrived at destination");
            }
        }
        
        private OrchestratorGridClaim OnReceiveRepairTask(RepairTask repairTask) {
            targetRemovedHandler = () => {
                repairTask.target.OnRemoved -= targetRemovedHandler;
                OnTaskEnded();
            };

            repairTask.target.OnRemoved += targetRemovedHandler;

            claim = new OrchestratorGridClaim {
                gridAnchor = repairTask.target.GridAnchor
            };

            return claim;
        }

        private void OnTaskEnded() {
            targetRemovedHandler = null;
            task = null;

            if (claim != null) {
                parent.SupportDroneOrchestrator.GridClaims.Remove(claim);
            }

            claim = null;
        }

        public TakenDamage TakeDamage(Damage damage) {
            if (!healthPool.TakeDamage(damage, data.armor, out var takenDamage)) {
                OnTaskEnded();
                parent.SupportDroneOrchestrator.OnDestroy(this);
            }
            
            return takenDamage;
        }
    }
}