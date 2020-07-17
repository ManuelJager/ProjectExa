using DG.Tweening;
using Exa.UI;
using Exa.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blueprints.Editor
{
    /// <summary>
    /// Grid layer for the ship layer
    /// </summary>
    public partial class EditorGrid : MonoBehaviour, IUIGroup
    {
        [SerializeField] private float movementSpeed;
        private bool interactible = true;
        private bool mirrorEnabled = false;
        private bool blockedByUI = false;
        private bool canPlaceGhost = false;
        private Vector2 centerPos;
        private Vector2 playerPos = Vector2.zero;
        private Vector2Int size;

        public EditorGridBackgroundLayer backgroundLayer;
        public EditorGridBlueprintLayer blueprintLayer;
        public EditorGridGhostLayer ghostLayer;

        public Vector2 MovementVector { private get; set; }
        public float ZoomScale { private get; set; }

        /// <summary>
        /// Wether or not the grid can be interacted with
        /// </summary>
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

        /// <summary>
        /// Wether of not a ghost mirror is enabled
        /// </summary>
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

        /// <summary>
        /// Wether or not the mouse is over UI
        /// </summary>
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
            backgroundLayer.EnterGrid += OnEnterGrid;
            backgroundLayer.ExitGrid += OnExitGrid;
        }

        public void Update()
        {
            if (!Interactable) return;

            // Move the grid to keyboard input
            // Remap zoom scale range to damp scale
            var remappedZoomScale = ZoomScale.Remap(0f, 3f, 0.5f, 1.5f);

            // Calculate movement offset
            playerPos -=
                MovementVector *
                movementSpeed *
                Time.deltaTime *
                remappedZoomScale;

            // Clamp movement offset to prevent going out of bounds
            playerPos = Vector2.ClampMagnitude(playerPos, 15f);

            // Get position by adding the pivot to the offset
            var position = centerPos + playerPos;
            transform.DOLocalMove(position, 0.3f);

            // Check for mouse input
            backgroundLayer.UpdateCurrActiveGridItem(transform.localPosition.ToVector2());
        }

        public void OnDisable()
        {
            // Reset values
            playerPos = Vector2.zero;
            mouseGridPos = null;
            transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Creates a grid with the given size
        /// </summary>
        /// <param name="size"></param>
        public void GenerateGrid(Vector2Int size)
        {
            // Set the active size and set targe player position the center of the grid
            this.size = size;

            // Set the movement pivot
            centerPos = GetGridOffset();
            transform.localPosition = centerPos.ToVector3();

            // Generate the grid
            backgroundLayer.GenerateGrid(size);
        }

        /// <summary>
        /// Import a blueprint
        /// </summary>
        /// <param name="blueprint"></param>
        public void Import(Blueprint blueprint)
        {
            // Get size of blueprint class and resize the grid accordingly
            var editorSize = blueprint.blueprintType.maxSize;
            GenerateGrid(editorSize);

            // Import the blueprint
            blueprintLayer.Import(blueprint);
        }

        public void ClearBlueprint()
        {
            blueprintLayer.ClearBlueprint();
        }

        private Vector2 GetGridOffset()
        {
            var halfSize = size.ToVector2() / 2f;
            return new Vector2
            {
                x = -halfSize.x + 0.5f,
                y = -halfSize.y + 0.5f
            };
        }

        /// <summary>
        /// Walk through all cases and calculate wether the ghost/s should be enabled
        /// </summary>
        public void CalculateGhostEnabled()
        {
            if (!ghostLayer.GhostCreated) return;

            if (BlockedByUI)
            {
                canPlaceGhost = false;
                return;
            }

            var ghostTiles = GridUtils.GetOccupiedTilesByAnchor(ghostLayer.ghost.AnchoredBlueprintBlock);
            var mirrorGhostTiles = GridUtils.GetOccupiedTilesByAnchor(ghostLayer.mirrorGhost.AnchoredBlueprintBlock);

            bool GetGhostIsClear(IEnumerable<Vector2Int> occupiedGhostTiles)
            {
                return !blueprintLayer.ActiveBlueprint.Blocks.HasOverlap(occupiedGhostTiles) &&
                    occupiedGhostTiles.All((gridPos) => backgroundLayer.PosIsInGrid(gridPos));
            }

            // Calculate wether the main ghost doesn't overlap existing blocks or is outside of the grid
            var ghostIsClear = GetGhostIsClear(ghostTiles);

            if (MirrorEnabled)
            {
                // Calculate wether the mirror ghost doesn't overlap existing blocks or is outside of the grid
                var mirrorGhostIsClear = GetGhostIsClear(mirrorGhostTiles);

                // Calculate if the ghosts intersect
                var ghostsIntersect = ghostTiles.Intersect(mirrorGhostTiles).Any() &&
                    !(ghostLayer.ghost.AnchoredBlueprintBlock.gridAnchor
                    == ghostLayer.mirrorGhost.AnchoredBlueprintBlock.gridAnchor);

                // Set the color for the main ghost
                ghostLayer.ghost.SetFilterColor(ghostIsClear && !ghostsIntersect);

                // Set the color for the mirror ghost
                ghostLayer.mirrorGhost.SetFilterColor(mirrorGhostIsClear && !ghostsIntersect);

                // Only allow block placement when both ghosts are clear
                canPlaceGhost = ghostIsClear && mirrorGhostIsClear && !ghostsIntersect;
            }
            else
            {
                // Set the filter color to the clear state of the ghost
                ghostLayer.ghost.SetFilterColor(ghostIsClear);
                canPlaceGhost = ghostIsClear;
            }
        }
    }
}