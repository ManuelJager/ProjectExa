using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public partial class EditorGrid
    {
        private Vector2Int? mouseGridPos;

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
            ghostLayer.OnRotateLeft();
            CalculateGhostEnabled();
        }

        public void OnRotateRight()
        {
            ghostLayer.OnRotateRight();
            CalculateGhostEnabled();
        }

        public void OnLeftClickPressed()
        {
            if (MouseGridPos == null) return;

            if (!canPlaceGhost) return;

            canPlaceGhost = false;

            blueprintLayer.AddBlock(
                ghostLayer.ghost.GridPos,
                ghostLayer.ghost.blueprintBlock);

            if (!MirrorEnabled || ghostLayer.ghost.GridPos == ghostLayer.mirrorGhost.GridPos) return;

            blueprintLayer.AddBlock(
                ghostLayer.mirrorGhost.GridPos,
                ghostLayer.mirrorGhost.blueprintBlock);
        }

        public void OnRightClickPressed()
        {
            if (MouseGridPos == null) return;

            var realGridPos = MouseGridPos.GetValueOrDefault();

            blueprintLayer.RemoveBlock(realGridPos);

            if (MirrorEnabled)
            {
                blueprintLayer.RemoveBlock(ShipEditorUtils.GetMirroredGridPos(size, realGridPos));
            }
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
                backgroundLayer.SetGridBackgroundItemColor(mirroredGridPos, mirrorEnabled && enter);
            });
        }
    }
}