using System.Collections.Generic;
using Exa.Gameplay;
using Exa.Math;
using Exa.Utils;
using NaughtyAttributes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class DroneBayBehaviour : BlockBehaviour<DroneBayData> {
        [SerializeField] private GameObject dronePrefab;
        [SerializeField] private float spawnHeight;
        
        private float remainingBuildTime;
        private List<SupportDrone> activeDrones;
        
        protected override void OnAdd() {
            remainingBuildTime = data.buildTime;
            activeDrones = new List<SupportDrone>(); // NOTE: As soon this list is created, any roaming drones should be attached to it
        }

        protected override void BlockUpdate() {
            if (remainingBuildTime == 0f && activeDrones.Count < data.maxPopulation) {
                DeployDrone();
                remainingBuildTime = data.buildTime;
            }
            
            remainingBuildTime = Mathf.Max(remainingBuildTime - Time.deltaTime, 0f);
        }

    #if UNITY_EDITOR
        [Button("Force deploy drone", enabledMode: EButtonEnableMode.Playmode)]
        private void ForceDeployDrone() {
            DeployDrone();
            remainingBuildTime = data.buildTime;
        }
    #endif

        private void DeployDrone() {
            var drone = dronePrefab.Create<SupportDrone>(transform);
            
            drone.Setup(this, GS.SpawnLayer.drones);
            
            var droneTransform = drone.transform;
            droneTransform.position = droneTransform.position.SetZ(spawnHeight);
            
            activeDrones.Add(drone);
        }

        public void OnDestroyDrone(SupportDrone supportDrone) {
            activeDrones.Remove(supportDrone);
        }
    }
}
