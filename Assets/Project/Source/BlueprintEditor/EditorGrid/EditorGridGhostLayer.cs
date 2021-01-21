 using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks;
 using Exa.Grids.Blocks.BlockTypes;
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
        public EditorGridTurretLayer gridTurretLayer;

        private IEnumerable<GhostController> controllers;

        public bool PlacementIsAllowed { get; private set; }
        public BlockTemplate ImportedTemplate { get; private set; }

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
            controllers.ForEach(controller => controller.Rotate(value));
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
            ImportedTemplate = template;

            var block = new BlueprintBlock {
                id = template.id,
                Rotation = 0
            };

            controllers.ForEach(controller => {
                controller.ImportBlock(block);

                var overlay = template is ITurretTemplate turretTemplate
                    ? gridTurretLayer.CreateGhostOverlay(controller.Ghost.Block, turretTemplate)
                    : null;
                controller.SetOverlay(overlay);
            });
         }

        public void MoveGhost(Vector2Int gridSize, Vector2Int? gridPos) {
            SetVisibility(gridPos != null);

            if (!ImportedTemplate) {
                return;
            }
            
            controllers.ForEach(controller => controller.MoveGhost(gridSize, gridPos));

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

            var activeControllers = QueryActiveControllers();
            SetFlags(controllers.Except(activeControllers), true);
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
                .SelectMany(controller => controller.BlueprintBlock.GetOccupiedTiles());

            var positionValid = occupiedBlocks.Distinct().Count() == occupiedBlocks.Count()
                   && occupiedBlocks.All(pos => backgroundLayer.PosIsInGrid(pos))
                   && !blueprintLayer.ActiveBlueprint.Blocks.HasOverlap(occupiedBlocks);

            if (!positionValid) {
                return false;
            }

            if (ImportedTemplate is ITurretTemplate) {
                var turretContacts = activeControllers.SelectMany(controller => controller.Overlay.GetContacts());
                return !turretContacts.Intersect(occupiedBlocks).Any();
            }

            return true;
        }

        private GhostController InitController(BlockFlip flip) {
            var ghost = Instantiate(ghostPrefab, transform).GetComponent<BlockGhost>();
            ghost.gameObject.name = $"GhostController (Flip: {flip})";
            ghost.gameObject.SetActive(false);
            return new GhostController(ghost, flip);
        }

        private IEnumerable<GhostController> QueryActiveControllers() {
            return controllers.Where(controller => controller.State.HasValue(GhostControllerState.Active))
                .Where(controller => controller.BlueprintBlock != null)
                .GroupBy(controller => controller.BlueprintBlock.gridAnchor)
                .Select(group => group.First());
        }

        private void SetFilter(bool active) {
            controllers.ForEach(controller => controller.SetFilterColor(active));
        }
    }
}