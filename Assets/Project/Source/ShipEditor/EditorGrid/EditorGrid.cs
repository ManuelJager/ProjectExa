using DG.Tweening;
using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.UI;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Exa.ShipEditor
{
    /// <summary>
    /// Grid layer for the Ship layer
    /// </summary>
    public partial class EditorGrid : MonoBehaviour, IUiGroup
    {
        [SerializeField] private float _movementSpeed;
        private bool _interactible = true;
        private bool _mirrorEnabled = false;
        private bool _mouseOverUi = false;
        private bool _canPlaceGhost = false;
        private Vector2 _centerPos;
        private Vector2 _playerPos = Vector2.zero;
        private Vector2Int _size;
        private TweenerCore<Vector3, Vector3, VectorOptions> _positionTweener;

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
            get => _interactible;
            set
            {
                _interactible = value;
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
            get => _mirrorEnabled;
            set
            {
                _mirrorEnabled = value;

                ghostLayer.MirrorEnabled = value;
                SetActiveBackground(_mouseGridPos, true);
                CalculateGhostEnabled();
            }
        }

        /// <summary>
        /// Wether or not the mouse is over UI
        /// </summary>
        public bool MouseOverUi
        {
            get => _mouseOverUi;
            set
            {
                _mouseOverUi = value;

                ghostLayer.MouseOverUi = value;
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
            _playerPos -=
                MovementVector *
                _movementSpeed *
                Time.deltaTime *
                remappedZoomScale;

            // Clamp movement offset to prevent going out of bounds
            _playerPos = Vector2.ClampMagnitude(_playerPos, 15f);

            // Get position by adding the pivot to the offset
            var position = _centerPos + _playerPos;

            _positionTweener?.Kill();
            _positionTweener = transform.DOLocalMove(position, 0.3f);

            // Check for mouse input
            backgroundLayer.UpdateCurrActiveGridItem(transform.localPosition.ToVector2());
        }

        public void OnDisable()
        {
            // Reset values
            _playerPos = Vector2.zero;
            _mouseGridPos = null;
            transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Creates a grid with the given size
        /// </summary>
        /// <param name="size"></param>
        public void GenerateGrid(Vector2Int size)
        {
            // Set the active size and set targe player position the center of the grid
            this._size = size;

            // Set the movement pivot
            _centerPos = GetGridOffset();
            transform.localPosition = _centerPos.ToVector3();

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
            var editorSize = blueprint.BlueprintType.maxSize;
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
            var halfSize = _size.ToVector2() / 2f;
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

            if (MouseOverUi)
            {
                _canPlaceGhost = false;
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
                _canPlaceGhost = ghostIsClear && mirrorGhostIsClear && !ghostsIntersect;
            }
            else
            {
                // Set the filter color to the clear state of the ghost
                ghostLayer.ghost.SetFilterColor(ghostIsClear);
                _canPlaceGhost = ghostIsClear;
            }
        }
    }
}