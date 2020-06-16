using Exa.UI;
using Exa.Utils;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public partial class EditorGrid : MonoBehaviour, IInteractableGroup
    {
        [SerializeField] private float movementSpeed;
        private bool interactible = true;
        private bool mirrorEnabled = false;
        private bool blockedByUI = false;
        private bool canPlaceGhost = false;
        private Vector2 playerPos = Vector2.zero;
        private Vector2Int size;

        public EditorGridBackgroundLayer backgroundLayer;
        public EditorGridGhostLayer ghostLayer;
        public EditorGridBlueprintLayer blueprintLayer;

        public Vector2 MovementVector { private get; set; }
        public bool Interactable
        {
            get => interactible;
            set
            {
                interactible = value;
                if (!value)
                {
                    ghostLayer.GhostVisible = false;
                }
            }
        }

        public bool MirrorEnabled
        {
            get => mirrorEnabled;
            set
            {
                mirrorEnabled = value;

                ghostLayer.MirrorEnabled = value;
                SetActiveBackground(mouseGridPos, true);
                CalculateGhostEnabled();
            }
        }

        public bool BlockedByUI
        {
            get => blockedByUI;
            set
            {
                blockedByUI = value;

                ghostLayer.BlockedByUI = value;
                CalculateGhostEnabled();
            }
        }

        private void Awake()
        {
            playerPos = GetGridOffset();
            backgroundLayer.EnterGrid += OnEnterGrid;
            backgroundLayer.ExitGrid += OnExitGrid;
        }

        public void Update()
        {
            if (!Interactable) return;
            
            // Move the grid to keyboard input
            // TODO: Smooth 
            playerPos -= MovementVector * movementSpeed * Time.deltaTime;
            transform.localPosition = playerPos.ToVector3();

            // Check for mouse input
            backgroundLayer.UpdateCurrActiveGridItem(playerPos);
        }

        public void OnDisable()
        {
            // Reset values
            playerPos = Vector2.zero;
            mouseGridPos = null;
            transform.localPosition = Vector3.zero;
        }

        // Create a grid
        public void GenerateGrid(Vector2Int size)
        {
            this.size = size;
            backgroundLayer.GenerateGrid(size);
            playerPos = GetGridOffset();
        }

        public void Import(Blueprint blueprint)
        {
            var editorSize = blueprint.blueprintType.maxSize;
            GenerateGrid(editorSize);
            blueprintLayer.Import(blueprint);
        }

        public Blueprint Export()
        {
            return blueprintLayer.Export();
        }

        public void ClearBlueprint()
        {
            blueprintLayer.ClearBlueprint();
        }

        private Vector2 GetGridOffset()
        {
            return new Vector2(-(size.x / 2f), -(size.y / 2f));
        }

        public void CalculateGhostEnabled()
        {
            if (!ghostLayer.GhostCreated) return;

            if (BlockedByUI)
            {
                canPlaceGhost = false;
                return;
            }

            var ghostTiles = ShipEditorUtils.GetOccupiedTilesByGhost(ghostLayer.ghost);
            var mirrorGhostTiles = ShipEditorUtils.GetOccupiedTilesByGhost(ghostLayer.mirrorGhost);

            if (MirrorEnabled)
            {
                var ghostIsClear =
                (
                    !blueprintLayer.ActiveBlueprint.blocks.HasOverlap(ghostTiles) &&
                    ghostTiles.All((gridPos) => backgroundLayer.PosIsInGrid(gridPos))
                );

                var mirrorGhostIsClear =
                (
                    !blueprintLayer.ActiveBlueprint.blocks.HasOverlap(mirrorGhostTiles) &&
                    mirrorGhostTiles.All((gridPos) => backgroundLayer.PosIsInGrid(gridPos))
                );

                var ghostsIntersect = ghostTiles.Intersect(mirrorGhostTiles).Any() &&
                    !(ghostLayer.ghost.GridPos == ghostLayer.mirrorGhost.GridPos);

                ghostLayer.ghost.SetFilterColor(ghostIsClear && !ghostsIntersect);
                ghostLayer.mirrorGhost.SetFilterColor(mirrorGhostIsClear && !ghostsIntersect);
                canPlaceGhost = (ghostIsClear && mirrorGhostIsClear && !ghostsIntersect);
            }
            else
            {
                var ghostIsClear =
                (
                    !blueprintLayer.ActiveBlueprint.blocks.HasOverlap(ghostTiles) &&
                    ghostTiles.All((gridPos) => backgroundLayer.PosIsInGrid(gridPos))
                );

                ghostLayer.ghost.SetFilterColor(ghostIsClear);
                canPlaceGhost = ghostIsClear;
            }
        }
    }
}