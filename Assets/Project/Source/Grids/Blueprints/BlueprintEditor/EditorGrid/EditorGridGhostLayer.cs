using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class EditorGridGhostLayer : MonoBehaviour
    {
        private bool ghostVisible = true;
        private bool mirrorEnabled = false;
        private bool blockedByUI = false;

        public GameObject ghostPrefab;
        public BlockGhost ghost;
        public BlockGhost mirrorGhost;

        public bool GhostCreated { get; private set; }

        /// <summary>
        /// Ghost was created
        /// </summary>
        public bool GhostVisible
        {
            get => ghostVisible;
            set
            {
                ghostVisible = value;

                CalculateGhostEnabled();
            }
        }

        /// <summary>
        /// Ghost is enabled
        /// </summary>
        public bool MirrorEnabled
        {
            get => mirrorEnabled;
            set
            {
                mirrorEnabled = value;

                CalculateGhostEnabled();
            }
        }

        /// <summary>
        /// If player is hovering over ui
        /// </summary>
        public bool BlockedByUI
        {
            get => blockedByUI;
            set
            {
                blockedByUI = value;

                CalculateGhostEnabled();
            }
        }

        private void Awake()
        {
            ghost = Instantiate(ghostPrefab, transform).GetComponent<BlockGhost>();
            mirrorGhost = Instantiate(ghostPrefab, transform).GetComponent<BlockGhost>();

            GhostCreated = false;
            GhostVisible = false;
        }

        private void OnDisable()
        {
            GhostVisible = false;
        }

        public void CreateGhost(BlockTemplate template)
        {
            ghost.ImportBlock(new BlueprintBlock
            {
                id = template.Id,
                Rotation = 0,
                flippedX = false,
                flippedY = false
            });

            mirrorGhost.ImportBlock(new BlueprintBlock
            {
                id = template.Id,
                Rotation = 0,
                flippedX = false,
                flippedY = true
            });

            GhostCreated = true;
        }

        public void MoveGhost(Vector2Int gridSize, Vector2Int? anchorPos)
        {
            if (!GhostCreated) return;

            GhostVisible = anchorPos != null;

            if (!GhostVisible) return;

            var realAnchorPos = anchorPos.GetValueOrDefault();
            var mirroredAnchorPos = ShipEditorUtils.GetMirroredGridPos(gridSize, realAnchorPos);
            ghost.GridPos = realAnchorPos;
            mirrorGhost.GridPos = mirroredAnchorPos;

            CalculateGhostEnabled();
        }

        public void OnRotateLeft()
        {
            ghost.Rotation += 1;
            mirrorGhost.Rotation -= 1;
        }

        public void OnRotateRight()
        {
            ghost.Rotation -= 1;
            mirrorGhost.Rotation += 1;
        }

        private void CalculateGhostEnabled()
        {
            ghost.gameObject.SetActive(
                GhostVisible &&
                !BlockedByUI);

            mirrorGhost.gameObject.SetActive(
                MirrorEnabled &&
                GhostVisible &&
                ghost.GridPos != mirrorGhost.GridPos
                && !BlockedByUI);
        }
    }
}