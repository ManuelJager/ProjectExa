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

#pragma warning disable CS0649

namespace Exa.Ships
{
    public abstract class Ship : MonoBehaviour, IRaycastTarget, ITooltipPresenter, IDebugDragable, IGridInstance
    {
        [Header("References")]
        [SerializeField] private Transform pivot;
        [SerializeField] private ShipAi shipAi;
        [SerializeField] private ShipState state;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CircleCollider2D mouseOverCollider;
        [SerializeField] private NavigationOptions navigationOptions;

        [Header("Settings")] 
        [SerializeField] private ValueOverride<CursorState> cursorOverride;
        public float canvasScaleMultiplier = 1f;
        public Font shipDebugFont;

        private Tooltip debugTooltip;

        [Header("Events")]
        public UnityEvent ControllerDestroyedEvent;

        public ActionScheduler ActionScheduler { get; private set; }
        public BlockContext BlockContext { get; private set; }
        public BlockGrid BlockGrid { get; private set; }
        public Blueprint Blueprint { get; private set; }
        public Controller Controller { get; internal set; }
        public INavigation Navigation { get; private set; }
        public ShipGridTotals Totals { get; private set; }
        public bool Active { get; private set; }
        public ShipOverlay Overlay { get; set; }
        public Transform Transform => transform;
        public Rigidbody2D Rigidbody2D => rb;
        public ShipAi Ai => shipAi;
        public ShipState State => state;

        protected virtual void Awake() {
            debugTooltip = new Tooltip(GetDebugTooltipComponents, shipDebugFont);
        }

        private void FixedUpdate() {
            if (Active) {
                var deltaTime = Time.fixedDeltaTime;
                Navigation?.Update(deltaTime);
                ActionScheduler.ExecuteActions(deltaTime);
            }
            
            mouseOverCollider.offset = rb.centerOfMass;

            if (debugTooltip != null) {
                debugTooltip.ShouldRefresh = true;
            }
        }

        // TODO: Make this look nicer by breaking up the ship and adding an explosion
        public virtual void OnControllerDestroyed() {
            ControllerDestroyedEvent?.Invoke();
            Active = false;

            foreach (var thruster in BlockGrid.Metadata.QueryByType<IThruster>()) {
                thruster.PowerDown();
            }
        }

        public virtual void Import(Blueprint blueprint, BlockContext blockContext) {
            if (blueprint.Blocks.Controller == null) {
                throw new ArgumentException("Blueprint must have a controller reference");
            }

            Totals = new ShipGridTotals(this);
            BlockGrid = new BlockGrid(pivot, OnGridEmpty, this);
            ActionScheduler = new ActionScheduler(this);
            Active = true;
            BlockContext = blockContext;

            var radius = blueprint.Blocks.MaxSize / 2f * canvasScaleMultiplier;
            mouseOverCollider.radius = radius;
            Navigation = navigationOptions.GetNavigation(this, blueprint);
            BlockGrid.Import(blueprint);
            Blueprint = blueprint;

            UpdateCanvasSize(blueprint);

            shipAi.Init();
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
        public IEnumerable<Ship> QueryNeighbours(float radius, ShipMask shipMask, bool mustBeActive = false) {
            var colliders = Physics2D.OverlapCircleAll(transform.position, radius, shipMask.LayerMask);

            foreach (var collider in colliders) {
                var neighbour = collider.gameObject.GetComponent<Ship>();
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

        private void UpdateCanvasSize(Blueprint blueprint) {
            var size = blueprint.Blocks.MaxSize * 10 * canvasScaleMultiplier;
            Overlay.rectContainer.sizeDelta = new Vector2(size, size);
        }

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
            new TooltipGroup(state.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("AI:"),
            new TooltipGroup(shipAi.GetDebugTooltipComponents(), 1)
        });

        public Vector2 GetDebugDraggerPosition() {
            return transform.position;
        }

        public Vector2 GetPosition() {
            return Rigidbody2D.worldCenterOfMass;
        }

        public void SetDebugDraggerGlobals(Vector2 position, Vector2 velocity) {
            transform.position = position;
            rb.velocity = velocity;
        }

        private void OnGridEmpty() {
            if (gameObject)
                Destroy(gameObject);
        }
    }
}