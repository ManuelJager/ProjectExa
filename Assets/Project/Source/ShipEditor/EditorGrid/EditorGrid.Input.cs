using Exa.Grids;
using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.ShipEditor
{
    public partial class EditorGrid
    {
        private Vector2Int? _mouseGridPos;

        /// <summary>
        /// Current mouse position in the grid
        /// <para>
        /// A null value signifies a mouse outside of the grid
        /// </para>
        /// </summary>
        private Vector2Int? MouseGridPos
        {
            get => _mouseGridPos;
            set
            {
                if (_mouseGridPos == value) return;

                if (_mouseGridPos != null)
                {
                    SetActiveBackground(_mouseGridPos, false);
                }

                _mouseGridPos = value;

                if (value != null)
                {
                    SetActiveBackground(value, true);
                }

                ghostLayer.MoveGhost(_size, value);
                CalculateGhostEnabled();
            }
        }

        public void OnBlockSelected(BlockTemplate template)
        {
            ghostLayer.CreateGhost(template);
        }

        public void OnRotateLeft()
        {
            if (!ghostLayer.GhostCreated) return;

            ghostLayer.RotateGhosts(1);
            CalculateGhostEnabled();
        }

        public void OnRotateRight()
        {
            if (!ghostLayer.GhostCreated) return;

            ghostLayer.RotateGhosts(-1);
            CalculateGhostEnabled();
        }

        public void OnLeftClickPressed()
        {
            // if mouse position is invalid
            if (MouseGridPos == null) return;

            // if ghost cannot be placed
            if (!_canPlaceGhost) return;

            // if the editor grid is interactable
            if (!Interactable) return;

            _canPlaceGhost = false;

            blueprintLayer.AddBlock(ghostLayer.ghost.AnchoredBlueprintBlock.Clone());

            if (MirrorEnabled && ghostLayer.ghost.AnchoredBlueprintBlock.GridAnchor
                != ghostLayer.mirrorGhost.AnchoredBlueprintBlock.GridAnchor)
            {
                blueprintLayer.AddBlock(ghostLayer.mirrorGhost.AnchoredBlueprintBlock.Clone());
            }

            CalculateGhostEnabled();
        }

        public void OnRightClickPressed()
        {
            if (MouseGridPos == null) return;

            if (!Interactable) return;

            var realGridPos = MouseGridPos.GetValueOrDefault();

            blueprintLayer.RemoveBlock(realGridPos);

            if (MirrorEnabled)
            {
                blueprintLayer.RemoveBlock(GridUtils.GetMirroredGridPos(_size, realGridPos));
            }

            CalculateGhostEnabled();
        }

        private void OnEnterGrid(Vector2Int? gridPos)
        {
            MouseGridPos = gridPos;
        }

        private void OnExitGrid()
        {
            MouseGridPos = null;
        }

        private void SetActiveBackground(Vector2Int? gridPos, bool enter)
        {
            if (gridPos == null) return;

            backgroundLayer.SetGridBackgroundItemColor(gridPos, enter);

            GridUtils.ConditionallyApplyToMirror(gridPos, _size, (mirroredGridPos) =>
            {
                backgroundLayer.SetGridBackgroundItemColor(mirroredGridPos, MirrorEnabled && enter);
            });
        }
    }
}