﻿using DG.Tweening;
using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.UI;
using System.Collections.Generic;
using System.Linq;
using Exa.UI.Tweening;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    /// <summary>
    /// Grid layer for the Ship layer
    /// </summary>
    public partial class EditorGrid : MonoBehaviour, IUIGroup
    {
        [SerializeField] private float movementSpeed;
        private bool interactable = true;
        private Vector2 centerPos;
        private Vector2 playerPos = Vector2.zero;
        private Vector2Int size;
        private TweenWrapper<Vector3> positionTween;

        public EditorGridBackgroundLayer backgroundLayer;
        public EditorGridBlueprintLayer blueprintLayer;
        public EditorGridGhostLayer ghostLayer;
        public EditorGridTurretLayer turretLayer;

        public Vector2 MovementVector { private get; set; }
        public float ZoomScale { private get; set; }

        /// <summary>
        /// Whether or not the grid can be interacted with
        /// </summary>
        public bool Interactable {
            get => interactable;
            set {
                interactable = value;
                if (!value) {
                    ghostLayer.SetVisibility(false);
                }
            }
        }

        /// <summary>
        /// Whether or not the mouse is over UI
        /// </summary>
        public bool MouseOverUI { get; set; }

        private void Awake() {
            backgroundLayer.EnterGrid += OnEnterGrid;
            backgroundLayer.ExitGrid += OnExitGrid;
            positionTween = new TweenWrapper<Vector3>((e, t) => transform.DOLocalMove(e, t));
        }

        public void Update() {
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

            positionTween.To(position, 0.3f);

            // Check for mouse input
            backgroundLayer.UpdateCurrActiveGridItem(transform.localPosition.ToVector2());
        }

        public void OnDisable() {
            // Reset values
            playerPos = Vector2.zero;
            mouseGridPos = null;
            transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Creates a grid with the given size
        /// </summary>
        /// <param name="size"></param>
        public void GenerateGrid(Vector2Int size) {
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
        public void Import(Blueprint blueprint) {
            // Get size of blueprint class and resize the grid accordingly
            var editorSize = blueprint.BlueprintType.maxSize;
            GenerateGrid(editorSize);

            // Import the blueprint
            blueprintLayer.Import(blueprint);
        }

        public void ClearBlueprint() {
            blueprintLayer.ClearBlueprint();
        }

        private Vector2 GetGridOffset() {
            var halfSize = size.ToVector2() / 2f;
            return new Vector2 {
                x = -halfSize.x + 0.5f,
                y = -halfSize.y + 0.5f
            };
        }
    }
}