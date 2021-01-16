using Exa.Grids;
using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.ShipEditor
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
        private Vector2Int? MouseGridPos {
            get => mouseGridPos;
            set {
                if (mouseGridPos == value) return;

                if (mouseGridPos != null) {
                    SetActiveBackground(mouseGridPos, false);
                }

                mouseGridPos = value;

                if (value != null) {
                    SetActiveBackground(value, true);
                }

                ghostLayer.MoveGhost(size, value);
            }
        }

        public void OnBlockSelected(BlockTemplate template) {
            ghostLayer.ImportTemplate(template);
        }

        public void OnRotateLeft() {
            ghostLayer.RotateGhosts(1);
        }

        public void OnRotateRight() {
            ghostLayer.RotateGhosts(-1);
        }

        public void OnLeftClickPressed() {
            if (MouseGridPos == null || !Interactable || MouseOverUI) {
                return;
            }

            ghostLayer.TryPlace();
        }

        public void OnRightClickPressed() {
            if (MouseGridPos == null || !Interactable) {
                return;
            }

            ghostLayer.TryDelete();
        }

        private void OnEnterGrid(Vector2Int? gridPos) {
            MouseGridPos = gridPos;
        }

        private void OnExitGrid() {
            MouseGridPos = null;
        }

        private void SetActiveBackground(Vector2Int? gridPos, bool enter) {
            if (gridPos == null) return;

            backgroundLayer.SetGridBackgroundItemColor(gridPos, enter);
        }
    }
}