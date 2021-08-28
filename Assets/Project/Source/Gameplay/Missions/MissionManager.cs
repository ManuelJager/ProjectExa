using System;
using Exa.Camera;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.IO;
using Exa.ShipEditor;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions {
    public struct EditResult {
        public Blueprint blueprint;
        // NOTE: This should be recalculated if the research context changes
        public BlockCosts editCost;
    }

    public class MissionManager : MonoBehaviour {
        private EditResult? editResult;
        public bool IsEditing { get; private set; }
        public Mission Mission { get; private set; }
        public BlockCosts CurrentResources { get; internal set; }
        public PlayerStation Station { get; internal set; }
        public MissionStats Stats { get; internal set; }

        public void Update() {
            if (Mission != null) {
                Mission.Update();
            }

            GS.UI.gameplayLayer.currentResources.Refresh(CurrentResources);
        }

        public void LoadMission(Mission mission) {
            if (Mission != null) {
                throw new InvalidOperationException("Cannot load a mission without unloading previous one");
            }

            Mission = mission;
            Stats = new MissionStats();
            mission.Init(manager: this);
        }

        public void AddResources(BlockCosts resources) {
            CurrentResources += resources;
        }

        public void UnloadMission() {
            Mission.Unload();
            Mission = null;
        }

        public void StartEditing() {
            IsEditing = true;
            editResult = null;

            var currentTarget = S.CameraController.CurrentTarget;
            GS.SpawnLayer.SetLayerActive(false);
            GS.UI.gameplayLayer.NavigateTo(S.Editor.navigateable);

            var settings = new BlueprintImportArgs(
                blueprint: Station.Blueprint,
                save: blueprint => {
                    var newCosts = blueprint.Grid.GetTotals(BlockContext.UserGroup).Metadata.blockCosts;
                    var oldCosts = Station.BlueprintTotals().Metadata.blockCosts;

                    editResult = new EditResult {
                        blueprint = blueprint,
                        editCost = newCosts - oldCosts
                    };
                }
            ) {
                OnExit = () => StopEditing(currentTarget)
            };

            settings.AddValidator(
                validator: new BlueprintCostValidator(
                    CurrentResources,
                    Station.BlueprintTotals().Metadata.blockCosts
                )
            );

            S.Editor.Import(settings);
        }

        private void StopEditing(ICameraTarget cameraTarget) {
            IsEditing = false;

            S.CameraController.SetTarget(cameraTarget);
            GS.SpawnLayer.SetLayerActive(true);

            if (editResult.GetHasValue(out var value)) {
                Debug.Log(new {
                    HasValue = true,
                    editResult = IOUtils.ToJson(value),
                });
                
                Station.SetBlueprint(value.blueprint);
                CurrentResources -= value.editCost;
            }
        }
    }
}