using System.Collections;
using System.Collections.Generic;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class DroneBayBehaviour : BlockBehaviour<DroneBayData> {
        [SerializeField] private GameObject dronePrefab;
        
        private float remainingBuildTime;
        private List<Drone> activeDrones;
        
        protected override void OnAdd() {
            remainingBuildTime = data.buildTime;
            activeDrones = new List<Drone>(); // NOTE: As soon this list is created, any roaming drones should be attached to it
        }

        protected override void BlockUpdate() {
            if (remainingBuildTime == 0f && activeDrones.Count < data.maxPopulation) {
                DeployDrone();
                remainingBuildTime = data.buildTime;
            }
            
            remainingBuildTime = Mathf.Max(remainingBuildTime - Time.deltaTime, 0f);
        }

        private void DeployDrone() {
            var drone = dronePrefab.Create<Drone>(transform);
            
            drone.Setup(this);
            
            activeDrones.Add(drone);
        }

        public void OnDestroyDrone(Drone drone) {
            activeDrones.Remove(drone);
        }
    }
}
