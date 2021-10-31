using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay {
    public class SupportDroneOrchestrator {
        private readonly IGridInstance instance;
        private readonly BlockGridDiff diff;

        public List<SupportDrone> CurrentDrones { get; }
        public OrchestratorGridClaims GridClaims { get; }

        public SupportDroneOrchestrator(IGridInstance instance, BlockGridDiff diff) {
            this.instance = instance;
            this.diff = diff;
            
            CurrentDrones = new List<SupportDrone>();
            GridClaims = new OrchestratorGridClaims();
        }

        public void Update() {
            var drones = GetAvailableDrones().ToList();

            if (drones.Count > 0) {
                var pendingRepair = GetPendingRepair().ToList();
                
                foreach (var drone in drones) {
                    var sortedPendingRepair = SortByDistance(pendingRepair, drone.transform.position);
                    var selectedToRepair = GridClaims.Except(sortedPendingRepair).FirstOrDefault();

                    if (selectedToRepair != null) {
                        var task = new RepairTask(selectedToRepair);
                        var claim = drone.SetTask(task);
                        GridClaims.Add(claim);
                    }
                }
            }
        }

        public void SubmitDrone(SupportDrone drone) {
            CurrentDrones.Add(drone);
        }

        public void OnDestroy(SupportDrone drone) {
            CurrentDrones.Remove(drone);
        }

        private IEnumerable<Block> GetPendingRepair() {
            return instance.BlockGrid.Where(block => !block.PhysicalBehaviour.IsRepaired);
        }

        private IEnumerable<T> SortByDistance<T>(IEnumerable<T> source, Vector2 pivot)
            where T : IGridMember {
            // Gets the magnitude between the world position of the grid member, and the pivot (Which is the world position of the drone)
            float Selector(T item) {
                return (item.GetGlobalPosition(instance) - pivot).magnitude;
            }
            
            return source.OrderBy(Selector);
        }

        private IEnumerable<SupportDrone> GetAvailableDrones() {
            return CurrentDrones.Where(drone => !drone.IsBusy);
        }
    }
}