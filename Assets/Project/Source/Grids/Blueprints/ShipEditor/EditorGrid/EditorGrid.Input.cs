using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Grids.Blueprints.Editor
{
    public partial class EditorGrid
    {
        private Vector2Int? mouseGridPos;

        /// <summary>
        /// Current mouse position in the grid
        /// <para>
        /// A null value signifies a mouse outside of the grid
        /// </para>
        /// </summary>
        private Vector2Int? MouseGridPos
        {
            get => mouseGridPos;
            set
            {
                if (mouseGridPos == value) return;

                if (mouseGridPos != null)
                {
                    SetActiveBackground(mouseGridPos, false);
                }

                mouseGridPos = value;

                if (value != null)
                {
                    SetActiveBackground(value, true);
                }

                ghostLayer.MoveGhost(size, value);
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

            ghostLayer.OnRotateLeft();
            CalculateGhostEnabled();
        }

        public void OnRotateRight()
        {
            if (!ghostLayer.GhostCreated) return;

            ghostLayer.OnRotateRight();
            CalculateGhostEnabled();
        }

        public void OnLeftClickPressed()
        {
            // if mouse position is invalid
            if (MouseGridPos == null) return;

            // if ghost cannot be placed
            if (!canPlaceGhost) return;

            // if the editor grid is interactable
            if (!Interactable) return;

            canPlaceGhost = false;

            blueprintLayer.AddBlock(ghostLayer.ghost.AnchoredBlueprintBlock);

            if (MirrorEnabled && ghostLayer.ghost.GridPos != ghostLayer.mirrorGhost.GridPos)
            {
                blueprintLayer.AddBlock(ghostLayer.mirrorGhost.AnchoredBlueprintBlock);
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
                blueprintLayer.RemoveBlock(ShipEditorUtils.GetMirroredGridPos(size, realGridPos));
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

            ShipEditorUtils.ConditionallyApplyToMirror(gridPos, size, (mirroredGridPos) =>
            {
                backgroundLayer.SetGridBackgroundItemColor(mirroredGridPos, MirrorEnabled && enter);
            });
        }
    }
}