using System;
using Exa.Camera;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.ShipEditor;
using Exa.Validation;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public class MissionManager : MonoBehaviour
    {
        public bool IsEditing { get; set; }
        public Mission Mission { get; private set; }
        public BlockCosts CurrentResources { get; private set; }


        private Blueprint editResult;

        public void LoadMission(Mission mission, MissionArgs args) {
            if (Mission != null) {
                throw new InvalidOperationException("Cannot load a mission without unloading previous one");
            }

            Mission = mission;
            mission.Init(args);
        }

        public void AddResources(BlockCosts resources) {
            CurrentResources += resources;
        }

        public void UnloadMission() {
            Mission = null;
        }

        public void StartEditing() {
            IsEditing = true;
            editResult = null;
            
            var currentTarget = Systems.CameraController.CurrentTarget;
            GameSystems.SpawnLayer.SetLayerActive(false);
            GameSystems.UI.gameplayLayer.NavigateTo(Systems.Editor.navigateable);

            var import = new BlueprintImportArgs(Mission.Station.Blueprint, result => editResult = result) {
                OnExit = () => StopEditing(currentTarget),
            };

            import.AddValidator(new BlueprintCostValidator(
                CurrentResources,
                Mission.Station.GetBaseTotals().Metadata.blockCosts
            ));
            
            Systems.Editor.Import(import);
        }

        public void Update() {
            if (Mission != null) {
                Mission.Update();
            }
            
            GameSystems.UI.gameplayLayer.currentResources.Refresh(CurrentResources);
        }
        
        private void StopEditing(ICameraTarget cameraTarget) {
            IsEditing = false;
            
            Systems.CameraController.SetTarget(cameraTarget);
            GameSystems.SpawnLayer.SetLayerActive(true);
            
            // TODO: use edit result
            if (editResult != null) {
                Mission.Station.SetBlueprint(editResult);
            }
        }
    }
}