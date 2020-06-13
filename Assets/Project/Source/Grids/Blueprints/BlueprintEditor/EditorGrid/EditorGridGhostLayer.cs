using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class EditorGridGhostLayer : MonoBehaviour
    {
        public GameObject ghostPrefab;

        public BlockGhost ghost;
        public BlockGhost mirrorGhost;

        public bool GhostCreated { get; private set; }

        private bool ghostVisible = true;

        public bool GhostVisible
        {
            get => ghostVisible;
            set
            {
                ghost.gameObject.SetActive(value);
                mirrorGhost.gameObject.SetActive(value && MirrorEnabled);
                ghostVisible = value;
            }
        }

        private bool mirrorEnabled = false;

        public bool MirrorEnabled
        {
            get => mirrorEnabled;
            set
            {
                mirrorGhost.gameObject.SetActive(value && GhostVisible && ghost.GridPos != mirrorGhost.GridPos);
                mirrorEnabled = value;
            }
        }

        private void Awake()
        {
            ghost = Instantiate(ghostPrefab, transform).GetComponent<BlockGhost>();
            mirrorGhost = Instantiate(ghostPrefab, transform).GetComponent<BlockGhost>();

            GhostCreated = false;
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

            if (anchorPos == null)
            {
                GhostVisible = false;
                return;
            }
            else
            {
                GhostVisible = true;
            }

            var realAnchorPos = anchorPos.GetValueOrDefault();
            var mirroredAnchorPos = ShipEditorUtils.GetMirroredGridPos(gridSize, realAnchorPos);
            ghost.GridPos = realAnchorPos;

            if (realAnchorPos == mirroredAnchorPos)
            {
                mirrorGhost.GridPos = mirroredAnchorPos;
                mirrorGhost.gameObject.SetActive(false);
            }
            else
            {
                mirrorGhost.GridPos = mirroredAnchorPos;
                mirrorGhost.gameObject.SetActive(MirrorEnabled && GhostVisible);
            }
        }

        public void OnRotateLeft()
        {
            ghost.Rotation += 1;
            mirrorGhost.Rotation += 1;
        }

        public void OnRotateRight()
        {
            ghost.Rotation -= 1;
            mirrorGhost.Rotation -= 1;
        }
    }
}