using System;
using Exa.Camera;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.ShipEditor;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public class MissionManager : MonoBehaviour
    {
        public static bool IsEditing { get; set; }
        public static Mission Mission { get; private set; }

        private Blueprint editResult;

        public void LoadMission(Mission mission, MissionArgs args) {
            if (Mission != null) {
                throw new InvalidOperationException("Cannot load a mission without unloading previous one");
            }

            Mission = mission;
            mission.Init(args);
        }

        public void UnloadMission() {
            Mission = null;
        }

        public void StartEditing() {
            IsEditing = true;

            var currentTarget = Systems.CameraController.CurrentTarget;
            GameSystems.SpawnLayer.SetLayerActive(false);
            GameSystems.UI.gameplayLayer.navigateable.NavigateTo(Systems.Editor.navigateable);

            var import = new BlueprintImportArgs(Mission.Station.Blueprint, result => editResult = result) {
                OnExit = () => StopEditing(currentTarget)
            };
            
            Systems.Editor.Import(import);
        }

        public void StopEditing(ICameraTarget cameraTarget) {
            IsEditing = false;
            
            Systems.CameraController.SetTarget(cameraTarget);
            GameSystems.SpawnLayer.SetLayerActive(true);
            
            // TODO: use edit result
            editResult = null;
        }

        public void Update() {
            if (Mission != null) {
                Mission.Update();
            }
        }
    }
}