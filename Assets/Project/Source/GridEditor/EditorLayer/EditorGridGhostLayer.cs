using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Types;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class EditorGridGhostLayer : MonoBehaviour
    {
        public GameObject ghostPrefab;
        public EditorGridBackgroundLayer backgroundLayer;
        public EditorGridBlueprintLayer blueprintLayer;

        private IEnumerable<GhostController> controllers;

        public bool PlacementIsAllowed { get; private set; }
        public bool Initialized { get; private set; }

        private void Awake() {
            controllers = new List<GhostController> {
                InitController(BlockFlip.None),
                InitController(BlockFlip.FlipX),
                InitController(BlockFlip.FlipY),
                InitController(BlockFlip.Both)
            };
        }

        public void SetFlip(BlockFlip mask) {
            controllers.ForEach(controller => controller.SetActive(mask));
            UpdateGhostsOverlap();
        }

        public void RotateGhosts(int value) {
            controllers.ForEach(controller => {
                controller.Ghost.AnchoredBlueprintBlock.blueprintBlock.Rotation += value;
                controller.Ghost.ReflectState();
            });

            UpdateGhosts();
        }

        public void TryPlace() {
            if (!PlacementIsAllowed) return;

            QueryActiveControllers().ForEach(controller => {
                blueprintLayer.AddBlock(controller.BlueprintBlock.Clone());
            });

            UpdateGhosts();
        }

        public void TryDelete() {
            QueryActiveControllers().ForEach(controller => {
                blueprintLayer.RemoveBlock(controller.BlueprintBlock.GridAnchor);
            });

            UpdateGhosts();
        }

        public void ImportTemplate(BlockTemplate template) {
            if (!Initialized) {
                Initialized = true;
            }

            var block = new BlueprintBlock {
                id = template.id,
                Rotation = 0
            };

            controllers.ForEach((controller) => controller.ImportBlock(block));
        }

        public void MoveGhost(Vector2Int gridSize, Vector2Int? gridPos) {
            SetVisibility(gridPos != null);
            if (gridPos != null) {
                controllers.ForEach(controller => controller.MoveGhost(gridSize, gridPos.GetValueOrDefault()));
            }

            UpdateGhostsOverlap();
            UpdateGhosts();
        }

        public void SetVisibility(bool value) {
            controllers.ForEach(controller => {
                controller.State = controller.State.SetFlag(GhostControllerState.Visible, value);
            });
        }

        private void UpdateGhostsOverlap() {
            void SetFlags(IEnumerable<GhostController> controllers, bool value) {
                controllers.ForEach(controller => {
                    controller.State = controller.State.SetFlag(GhostControllerState.Overlapped, value);
                });
            }

            var activeControllers = QueryActiveControllers().ToList();
            SetFlags(controllers.Except(activeControllers).ToList(), true);
            SetFlags(activeControllers, false);
        }

        private void UpdateGhosts() {
            var ghostPlacementIsValid = GetBlockPlacementIsValid();
            PlacementIsAllowed = ghostPlacementIsValid;
            SetFilter(ghostPlacementIsValid);
        }

        private bool GetBlockPlacementIsValid() {
            var activeControllers = QueryActiveControllers();

            if (!activeControllers.Any()) {
                return false;
            }

            var occupiedBlocks = activeControllers
                .Select(controller => controller.BlueprintBlock.GetOccupiedTiles())
                .SelectMany(block => block);

            return occupiedBlocks.Distinct().Count() == occupiedBlocks.Count()
                   && occupiedBlocks.All(pos => backgroundLayer.PosIsInGrid(pos))
                   && !blueprintLayer.ActiveBlueprint.Blocks.HasOverlap(occupiedBlocks);
        }

        private GhostController InitController(BlockFlip flip) {
            var ghost = Instantiate(ghostPrefab, transform).GetComponent<BlockGhost>();
            ghost.gameObject.name = $"GhostController (Flip: {flip})";
            ghost.gameObject.SetActive(false);
            return new GhostController(ghost, flip);
        }

        private IEnumerable<GhostController> QueryActiveControllers() {
            return controllers.Where(controller => controller.State.HasValue(GhostControllerState.Active))
                .GroupBy(controller => controller.BlueprintBlock.gridAnchor)
                .Select(group => group.First());
        }

        private void SetFilter(bool active) {
            controllers.ForEach(controller => controller.SetFilterColor(active));
        }
    }
}