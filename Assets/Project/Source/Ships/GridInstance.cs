﻿using Exa.AI;
using Exa.Debugging;
using Exa.Gameplay;
using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.UI;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using Exa.Grids;
using Exa.Types.Generics;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#pragma warning disable CS0649

namespace Exa.Ships
{
    public abstract class GridInstance : MonoBehaviour, IRaycastTarget, ITooltipPresenter, IGridInstance
    {
        [Header("References")]
        [SerializeField] private Transform pivot;
        [SerializeField] private GridAi gridAi;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CircleCollider2D mouseOverCollider;

        [Header("Settings")] 
        [SerializeField] private ValueOverride<CursorState> cursorOverride;
        [FormerlySerializedAs("shipDebugFont")] public Font debugFont;

        [Header("Events")]
        public UnityEvent ControllerDestroyedEvent;
        
        private Tooltip debugTooltip;
        private bool hovered = false;
        private float hullIntegrity;

        public ActionScheduler ActionScheduler { get; private set; }
        public BlockContext BlockContext { get; private set; }
        public GridInstanceConfiguration Configuration { get; private set; }
        public BlockGrid BlockGrid { get; private set; }
        public Blueprint Blueprint { get; private set; }
        public Controller Controller { get; internal set; }
        public GridTotals Totals { get; private set; }
        public bool Active { get; private set; }
        public IGridOverlay Overlay { get; set; }
        public Transform Transform => transform;
        public Rigidbody2D Rigidbody2D => rb;
        public GridAi Ai => gridAi;

        public float HullIntegrity { get; set; }

        protected virtual void Awake() {
            debugTooltip = new Tooltip(GetDebugTooltipComponents, debugFont);
        }

        private void Update() {
            var currentHull = BlockGrid.Totals.Hull;
            var totalHull = Blueprint.Blocks.Totals.Hull;
            HullIntegrity = currentHull / totalHull;

            if (Active) {
                Overlay.SetHullFill(currentHull, totalHull);
            }
        }

        private void FixedUpdate() {
            if (Active) {
                ActiveFixedUpdate(Time.fixedDeltaTime);
            }
            
            mouseOverCollider.offset = rb.centerOfMass;

            if (debugTooltip != null) {
                debugTooltip.ShouldRefresh = true;
            }
        }

        protected virtual void ActiveFixedUpdate(float fixedDeltaTime) {
            ActionScheduler.ExecuteActions(fixedDeltaTime);
        }

        // TODO: Make this look nicer by breaking up the ship and adding an explosion
        public virtual void OnControllerDestroyed() {
            foreach (var thruster in BlockGrid.Metadata.QueryByType<IThruster>()) {
                thruster.PowerDown();
            }
            
            ControllerDestroyedEvent?.Invoke();
            Active = false;
            
            ExitRaycast();
        }

        public virtual void Import(Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration) {
            if (blueprint.Blocks.Controller == null) {
                throw new ArgumentException("Blueprint must have a controller reference");
            }

            Totals = new GridTotals();
            BlockGrid = new BlockGrid(pivot, this);
            Configuration = configuration;
            ActionScheduler = new ActionScheduler(this);
            Active = true;
            BlockContext = blockContext;

            var radius = blueprint.Blocks.MaxSize / 2f;
            mouseOverCollider.radius = radius;
            BlockGrid.Import(blueprint);
            Blueprint = blueprint;

            gridAi.Init();
        }

        public string GetInstanceString() {
            return $"({GetType().Name}) {Blueprint.name} : {gameObject.GetInstanceID()}";
        }

        public virtual void OnRaycastEnter() {
            if (!Active) return;

            EnterRaycast();
        }

        private void EnterRaycast() {
            hovered = true;

            (Overlay as GridOverlay)?.SetHovered(true);
            Systems.UI.MouseCursor.stateManager.Add(cursorOverride);

            if (DebugMode.Ships.IsEnabled()) {
                Systems.UI.Tooltips.shipAIDebugTooltip.Show(this);
            }
        }

        public virtual void OnRaycastExit() {
            if (!Active) return;

            ExitRaycast();
        }

        private void ExitRaycast() {
            hovered = false;

            (Overlay as GridOverlay)?.SetHovered(false);
            Systems.UI.MouseCursor.stateManager.Remove(cursorOverride);

            if (DebugMode.Ships.IsEnabled()) {
                Systems.UI.Tooltips.shipAIDebugTooltip.Hide();
            }
        }

        // TODO: Somehow cache this, or let the results come from a central manager
        public IEnumerable<GridInstance> QueryNeighbours(float radius, ContextMask contextMask, bool mustBeActive = false) {
            var colliders = Physics2D.OverlapCircleAll(transform.position, radius, contextMask.LayerMask);

            foreach (var collider in colliders) {
                var neighbour = collider.gameObject.GetComponent<GridInstance>();
                if (neighbour == null) {
                    continue;
                }

                var passesContextMask = contextMask.HasValue(neighbour.BlockContext);
                if (!ReferenceEquals(neighbour, this) && passesContextMask) {
                    if (mustBeActive && !neighbour.Active) {
                        continue;
                    }

                    yield return neighbour;
                }
            }
        }

        public Tooltip GetTooltip() {
            return debugTooltip;
        }

        public abstract ShipSelection GetAppropriateSelection(Formation formation);

        public abstract bool MatchesSelection(ShipSelection selection);

        public abstract Vector2 GetPosition();

        public abstract void SetPosition(Vector2 position);

        private TooltipGroup GetDebugTooltipComponents() => new TooltipGroup(new ITooltipComponent[] {
            new TooltipTitle(GetInstanceString(), false),
            new TooltipSpacer(),
            new TooltipText("Blueprint:"),
            new TooltipGroup(Blueprint.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("BlockGrid:"),
            new TooltipGroup(BlockGrid.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("State:"),
            new TooltipGroup(new ITooltipComponent[] {
                new TooltipText($"HullIntegrity: {HullIntegrity}"),
            }, 1),
            new TooltipSpacer(),
            new TooltipText("State:"),
            new TooltipGroup(new ITooltipComponent[] {
                new TooltipText($"Rotation: {rb.rotation}"),
                new TooltipText($"Clamped Rotation: {MathUtils.NormalizeAngle360(rb.rotation)}"), 
            }, 1),
            new TooltipSpacer(), 
            new TooltipText("AI:"),
            new TooltipGroup(gridAi.GetDebugTooltipComponents(), 1)
        });
        
        public void SetLookAt(Vector2 globalLookAt) {
            Rigidbody2D.rotation = (globalLookAt - GetPosition()).GetAngle();
        }

        public void SetRotation(float angle) {
            Rigidbody2D.rotation = angle;
        }

        public void Rotate(float degrees) {
            Rigidbody2D.rotation += degrees;
        }
    }
}