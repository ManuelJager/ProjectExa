using System;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    [Flags]
    public enum GhostControllerState
    {
        Visible = 1 << 0,
        Active = 1 << 1,
        Overlapped = 1 << 2,
        Valid = Visible | Active
    }

    public class GhostController
    {
        private BlockFlip flip;
        private GhostControllerState state;
        private TurretOverlayHandle turretOverlayHandle;

        public BlockGhost Ghost { get; }
        public AnchoredBlueprintBlock BlueprintBlock => Ghost.Block;

        public GhostControllerState State {
            get => state;
            set {
                state = value;
                Ghost.gameObject.SetActive(state == GhostControllerState.Valid);
                UpdateOverlay();
            }
        }

        public GhostController(BlockGhost blockGhost, BlockFlip flip) {
            this.Ghost = blockGhost;
            this.flip = flip;
        }

        public void SetActive(BlockFlip mask) {
            State = State.SetFlag(GhostControllerState.Active, mask.HasValue(flip));
        }

        public void SetFilterColor(bool active) {
            Ghost.SetFilterColor(active);
        }

        public void ImportBlock(BlueprintBlock block) {
            Ghost.ImportBlock(new BlueprintBlock {
                flippedX = flip.HasValue(BlockFlip.FlipX),
                flippedY = flip.HasValue(BlockFlip.FlipY),
                id = block.id,
                Rotation = block.Rotation
            });
        }

        public void SetOverlayHandle(TurretOverlayHandle turretOverlayHandle) {
            this.turretOverlayHandle?.Destroy();
            this.turretOverlayHandle = turretOverlayHandle;
            turretOverlayHandle?.Overlay.gameObject.SetActive(false);
            UpdateOverlay();
        }

        public void Rotate(int value) {
            Ghost.Block.blueprintBlock.Rotation += value;
            Ghost.Presenter.Present(Ghost.Block);

            UpdateOverlay();
        }

        public void MoveGhost(Vector2Int gridSize, Vector2Int? originalPos) {
            if (originalPos != null) {
                var gridPos = GetMirroredGridPos(gridSize, originalPos.Value);
                Ghost.Block.GridAnchor = gridPos;
                Ghost.Presenter.Present(Ghost.Block);

                turretOverlayHandle?.SetActive(true);
                UpdateOverlay();
            }
            else {
                turretOverlayHandle?.SetActive(false);
            }
        }

        public Vector2Int GetMirroredGridPos(Vector2Int gridSize, Vector2Int originalPos) {
            return GridUtils.GetMirroredGridPos(gridSize, originalPos, flip);
        }

        private void UpdateOverlay() {
            if (turretOverlayHandle != null) {
                turretOverlayHandle.Overlay.Presenter.Present(Ghost.Block);
                turretOverlayHandle.Overlay.gameObject.SetActive(state.HasValue(GhostControllerState.Valid));
            }
        }
    }
}