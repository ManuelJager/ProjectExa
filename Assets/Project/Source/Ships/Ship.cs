using Exa.AI;
using Exa.Debugging;
using Exa.Gameplay;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Ships.Navigation;
using Exa.UI;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Ships
{
    public abstract class Ship : MonoBehaviour, IRaycastTarget, ITooltipPresenter, IDebugDragable
    {
        [Header("References")]
        public Transform pivot;
        public ShipAi shipAi;
        public ShipState state;
        public Rigidbody2D rb;
        public CircleCollider2D mouseOverCollider;
        public NavigationOptions navigationOptions;

        [Header("Settings")]
        public float canvasScaleMultiplier = 1f;
        public Font shipDebugFont;
        [SerializeField] private CursorState _onHoverState;

        [HideInInspector] public ShipOverlay overlay;
        private Tooltip _debugTooltip;
        private CursorOverride _cursorOverride;

        [Header("Events")]
        public UnityEvent destroyEvent = new UnityEvent();

        public ActionScheduler ActionScheduler { get; private set; }
        public ShipContext BlockContext { get; private set; }
        public BlockGrid BlockGrid { get; private set; }
        public Blueprint Blueprint { get; private set; }
        public Controller Controller { get; internal set; }
        public INavigation Navigation { get; private set; }
        public ShipGridTotals Totals { get; private set; }
        public TurretList Turrets { get; private set; }

        protected virtual void Awake()
        {
            _debugTooltip = new Tooltip(GetDebugTooltipComponents, shipDebugFont);
            _cursorOverride = new CursorOverride(_onHoverState);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            Navigation?.Update(deltaTime);
            ActionScheduler.ExecuteActions(deltaTime);

            if (_debugTooltip != null)
            {
                _debugTooltip.ShouldRefresh = true;
            }
        }

        private void LateUpdate()
        {
            UpdateCentreOfMassPivot(true);
        }

        public virtual void Import(Blueprint blueprint, ShipContext shipContext)
        {
            if (blueprint.Blocks.Controller == null)
            {
                throw new ArgumentException("Blueprint must have a controller reference");
            }

            Totals = new ShipGridTotals(this);
            BlockGrid = new BlockGrid(pivot, this);
            ActionScheduler = new ActionScheduler(this);
            Turrets = new TurretList();
            BlockContext = shipContext;

            var radius = blueprint.Blocks.MaxSize / 2f * canvasScaleMultiplier;
            mouseOverCollider.radius = radius;
            Navigation = navigationOptions.GetNavigation(this, blueprint);
            BlockGrid.Import(blueprint, shipContext);
            Blueprint = blueprint;

            UpdateCanvasSize(blueprint);
            UpdateCentreOfMassPivot(false);

            shipAi.Initialize();
        }

        public string GetInstanceString()
        {
            return $"{Blueprint.name} : {gameObject.GetInstanceID()}";
        }

        public virtual void OnRaycastEnter()
        {
            overlay.overlayCircle.IsHovered = true;
            Systems.Ui.mouseCursor.AddOverride(_cursorOverride);

            if (DebugMode.Ships.GetEnabled())
            {
                Systems.Ui.tooltips.shipAiDebugTooltip.Show(this);
            }
        }

        public virtual void OnRaycastExit()
        {
            overlay.overlayCircle.IsHovered = false;
            Systems.Ui.mouseCursor.RemoveOverride(_cursorOverride);

            if (DebugMode.Ships.GetEnabled())
            {
                Systems.Ui.tooltips.shipAiDebugTooltip.Hide();
            }
        }

        // TODO: Somehow cache this, or let the results come from a central manager
        public IEnumerable<T> QueryNeighbours<T>(float radius, ShipMask shipMask)
            where T : Ship
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, radius, shipMask.LayerMask);

            foreach (var collider in colliders)
            {
                var neighbour = collider.gameObject.GetComponent<T>();
                var passesContextMask = (neighbour.BlockContext & shipMask.ContextMask) != 0;
                if (!ReferenceEquals(neighbour, this) && passesContextMask)
                {
                    yield return neighbour;
                }
            }
        }

        public Tooltip GetTooltip()
        {
            return _debugTooltip;
        }

        public abstract ShipSelection GetAppropriateSelection(Formation formation);

        public abstract bool MatchesSelection(ShipSelection selection);

        private void UpdateCentreOfMassPivot(bool updateSelf)
        {
            var comOffset = -BlockGrid.CentreOfMass.GetCentreOfMass();

            if (updateSelf)
            {
                var currentPosition = pivot.localPosition.ToVector2();
                var diff = currentPosition - comOffset;
                transform.localPosition += diff.ToVector3();
            }

            pivot.localPosition = comOffset;
        }

        private void UpdateCanvasSize(Blueprint blueprint)
        {
            var size = blueprint.Blocks.MaxSize * 10 * canvasScaleMultiplier;
            overlay.rectContainer.sizeDelta = new Vector2(size, size);
        }

        private TooltipGroup GetDebugTooltipComponents() => new TooltipGroup(new ITooltipComponent[]
        {
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

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void SetGlobals(Vector2 position, Vector2 velocity)
        {
            transform.position = position;
            rb.velocity = velocity;
        }
    }
}