using System;
using Exa.Camera;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.ShipEditor;
using Exa.Ships;
using Exa.Validation;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public struct EditResult
    {
        public Blueprint blueprint;
        // NOTE: This should be recalculated if the research context changes
        public BlockCosts editCost; 
    }
    
    public class MissionManager : MonoBehaviour
    {
        public bool IsEditing { get; private set; }
        public Mission Mission { get; private set; }
        public BlockCosts CurrentResources { get; internal set; }
        public BlockCosts TotalCollectedResources { get; internal set; }
        public PlayerStation Station { get; internal set; }

        private EditResult? editResult;

        public void LoadMission(Mission mission, MissionArgs args) {
            if (Mission != null) {
                throw new InvalidOperationException("Cannot load a mission without unloading previous one");
            }

            Mission = mission;
            mission.Init(this, args);
        }

        public void AddResources(BlockCosts resources) {
            CurrentResources += resources;
            TotalCollectedResources += resources;
        }

        public void UnloadMission() {
            Mission.Unload();
            Mission = null;
        }

        public void StartEditing() {
            IsEditing = true;
            editResult = null;
            
            var currentTarget = Systems.CameraController.CurrentTarget;
            GameSystems.SpawnLayer.SetLayerActive(false);
            GameSystems.UI.gameplayLayer.NavigateTo(Systems.Editor.navigateable);

            var settings = new BlueprintImportArgs(Station.Blueprint, blueprint => {
                var newCosts = blueprint.Grid.GetTotals(BlockContext.UserGroup).Metadata.blockCosts;
                var oldCosts = Station.GetBaseTotals().Metadata.blockCosts;
                
                editResult = new EditResult {
                    blueprint = blueprint,
                    editCost = newCosts - oldCosts
                };
            }) {
                OnExit = () => StopEditing(currentTarget)
            };

            settings.AddValidator(new BlueprintCostValidator(
                CurrentResources,
                Station.GetBaseTotals().Metadata.blockCosts
            ));
            
            Systems.Editor.Import(settings);
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
                Station.SetBlueprint(editResult.Value.blueprint);
                CurrentResources -= editResult.Value.editCost;
            }
        }
    }
}