using Exa.AI;
using Exa.Debugging;
using Exa.Gameplay;
using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Ships.Navigation;
using Exa.UI;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using Exa.Grids;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#pragma warning disable CS0649

namespace Exa.Ships
{
    public abstract class GridInstance : MonoBehaviour, IRaycastTarget, ITooltipPresenter, IDebugDragable, IGridInstance
    {
        [Header("References")]
        [SerializeField] private Transform pivot;
        [SerializeField] private GridAi gridAi;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CircleCollider2D mouseOverCollider;

        [Header("Settings")] 
        [SerializeField] private ValueOverride<CursorState> cursorOverride;
        public float canvasScaleMultiplier = 1f;
        [FormerlySerializedAs("shipDebugFont")] public Font debugFont;

        [Header("Events")]
        public UnityEvent ControllerDestroyedEvent;

        private Tooltip debugTooltip;
        private float hullIntegrity;

        public ActionScheduler ActionScheduler { get; private set; }
        public BlockContext BlockContext { get; private set; }
        public GridInstanceConfiguration Configuration { get; private set; }
        public BlockGrid BlockGrid { get; private set; }
        public Blueprint Blueprint { get; private set; }
        public Controller Controller { get; internal set; }
        public GridTotals Totals { get; private set; }
        public bool Active { get; private set; }
        public ShipOverlay Overlay { get; set; }
        public Transform Transform => transform;
        public Rigidbody2D Rigidbody2D => rb;
        public GridAi Ai => gridAi;

        public float HullIntegrity
        {
            get => hullIntegrity;
            set
            {
                hullIntegrity = value;

                if (Active)
                    Overlay?.overlayHullBar.SetFill(value);
            }
        }

        protected virtual void Awake() {
            debugTooltip = new Tooltip(GetDebugTooltipComponents, debugFont);
        }

        private void Update() {
            var currentHull = BlockGrid.Totals.Hull;
            var totalHull = Blueprint.Blocks.Totals.Hull;
            HullIntegrity = currentHull / totalHull;
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

        protected void ActiveFixedUpdate(float fixedDeltaTime) {
            ActionScheduler.ExecuteActions(fixedDeltaTime);
        }

        // TODO: Make this look nicer by breaking up the ship and adding an explosion
        public virtual void OnControllerDestroyed() {
            ControllerDestroyedEvent?.Invoke();
            Active = false;

            foreach (var thruster in BlockGrid.Metadata.QueryByType<IThruster>()) {
                thruster.PowerDown();
            }
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

            var radius = blueprint.Blocks.MaxSize / 2f * canvasScaleMultiplier;
            mouseOverCollider.radius = radius;
            BlockGrid.Import(blueprint);
            Blueprint = blueprint;

            UpdateCanvasSize(blueprint);

            gridAi.Init();
        }

        public string GetInstanceString() {
            return $"({GetType().Name}) {Blueprint.name} : {gameObject.GetInstanceID()}";
        }

        public virtual void OnRaycastEnter() {
            if (!Active) return;

            Overlay.overlayCircle.IsHovered = true;
            Systems.UI.mouseCursor.stateManager.Add(cursorOverride);

            if (DebugMode.Ships.IsEnabled()) {
                Systems.UI.tooltips.shipAIDebugTooltip.Show(this);
            }
        }

        public virtual void OnRaycastExit() {
            if (!Active) return;

            Overlay.overlayCircle.IsHovered = false;
            Systems.UI.mouseCursor.stateManager.Remove(cursorOverride);

            if (DebugMode.Ships.IsEnabled()) {
                Systems.UI.tooltips.shipAIDebugTooltip.Hide();
            }
        }

        // TODO: Somehow cache this, or let the results come from a central manager
        public IEnumerable<GridInstance> QueryNeighbours(float radius, ShipMask shipMask, bool mustBeActive = false) {
            var colliders = Physics2D.OverlapCircleAll(transform.position, radius, shipMask.LayerMask);

            foreach (var collider in colliders) {
                var neighbour = collider.gameObject.GetComponent<GridInstance>();
                if (neighbour == null) {
                    continue;
                }

                var passesContextMask = (neighbour.BlockContext & shipMask.ContextMask) != 0;
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

        public Vector2 GetPosition() {
            return Rigidbody2D.worldCenterOfMass;
        }

        public void SetPosition(Vector2 position) {
            transform.position = position - Rigidbody2D.centerOfMass;
        }

        public Vector2 GetDebugDraggerPosition() {
            return transform.position;
        }

        public void SetDebugDraggerGlobals(Vector2 position, Vector2 velocity) {
            transform.position = position;
            rb.velocity = velocity;
        }

        public void Rotate(float degrees) {
            Rigidbody2D.rotation += degrees;
        }

        private void UpdateCanvasSize(Blueprint blueprint) {
            var size = blueprint.Blocks.MaxSize * 10 * canvasScaleMultiplier;
            Overlay.rectContainer.sizeDelta = new Vector2(size, size);
        }
    }
}