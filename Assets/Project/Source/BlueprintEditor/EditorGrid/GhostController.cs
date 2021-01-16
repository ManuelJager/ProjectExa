using System;
using Exa.Grids;
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
        private bool isOverlapped;
        private bool visible;
        private BlockFlip flip;
        private GhostControllerState state;

        public BlockGhost Ghost { get; }
        public AnchoredBlueprintBlock BlueprintBlock => Ghost.AnchoredBlueprintBlock;

        public GhostControllerState State {
            get => state;
            set {
                state = value;
                Ghost.gameObject.SetActive(state == GhostControllerState.Valid);
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

        public void MoveGhost(Vector2Int gridSize, Vector2Int originalPos) {
            var gridPos = GetMirroredGridPos(gridSize, originalPos);
            Ghost.AnchoredBlueprintBlock.GridAnchor = gridPos;
            Ghost.ReflectState();
        }

        public Vector2Int GetMirroredGridPos(Vector2Int gridSize, Vector2Int originalPos) {
            return GridUtils.GetMirroredGridPos(gridSize, originalPos, flip);
        }
    }
}