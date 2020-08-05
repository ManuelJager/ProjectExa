using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Grids.Blueprints.Editor
{
    public class EditorGridGhostLayer : MonoBehaviour
    {
        private bool ghostVisible = true;
        private bool mirrorEnabled = false;
        private bool mouseOverUI = false;

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
        public bool MouseOverUI
        {
            get => mouseOverUI;
            set
            {
                mouseOverUI = value;

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
                id = template.id,
                Rotation = 0,
                flippedX = false,
                flippedY = false
            });

            mirrorGhost.ImportBlock(new BlueprintBlock
            {
                id = template.id,
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
            var mirroredAnchorPos = GridUtils.GetMirroredGridPos(gridSize, realAnchorPos);

            ghost.AnchoredBlueprintBlock.gridAnchor = realAnchorPos;
            ghost.ReflectState();
            mirrorGhost.AnchoredBlueprintBlock.gridAnchor = mirroredAnchorPos;
            mirrorGhost.ReflectState();

            CalculateGhostEnabled();
        }

        public void RotateGhosts(int value)
        {
            ghost.AnchoredBlueprintBlock.blueprintBlock.Rotation += value;
            ghost.ReflectState();
            mirrorGhost.AnchoredBlueprintBlock.blueprintBlock.Rotation += value;
            mirrorGhost.ReflectState();
        }

        private void CalculateGhostEnabled()
        {
            ghost.gameObject.SetActive(
                GhostVisible &&
                !mouseOverUI);

            mirrorGhost.gameObject.SetActive(
                MirrorEnabled &&
                GhostVisible &&
                ghost.AnchoredBlueprintBlock.gridAnchor != mirrorGhost.AnchoredBlueprintBlock.gridAnchor
                && !mouseOverUI);
        }
    }
}