using Exa.AI;
using Exa.Debugging;
using Exa.Gameplay;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Ships.Navigations;
using Exa.UI;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Ships
{
    public abstract class Ship : MonoBehaviour, IRaycastTarget, ITooltipPresenter
    {
        [Header("References")]
        public Transform pivot;
        public ShipAI shipAI;
        public ShipState state;
        public Rigidbody2D rb;
        public CircleCollider2D mouseOverCollider;
        public NavigationOptions navigationOptions;

        [Header("Settings")]
        public float canvasScaleMultiplier = 1f;
        public Font shipDebugFont;
        [SerializeField] private CursorState onHoverState;

        [HideInInspector] public ShipOverlay overlay;
        public ShipNavigation navigation;
        public BlockGrid blockGrid;
        protected Blueprint blueprint;
        private Tooltip debugTooltip;
        private CursorOverride cursorOverride;

        [Header("Events")]
        public UnityEvent destroyEvent = new UnityEvent();

        public Blueprint Blueprint => blueprint;
        public ActionScheduler ActionScheduler { get; private set; }
        public Controller Controller { get; internal set; }
        public TurretList Turrets { get; private set; }
        public ShipContext BlockContext { get; private set; }

        protected virtual void Awake()
        {
            debugTooltip = new Tooltip(GetDebugTooltipComponents, shipDebugFont);
            cursorOverride = new CursorOverride(onHoverState, this);
        }

        private void FixedUpdate()
        {
            navigation?.ScheduledFixedUpdate();
            ActionScheduler.ExecuteActions(Time.fixedDeltaTime);

            if (debugTooltip != null)
            {
                debugTooltip.ShouldRefresh = true;
            }
        }

        private void LateUpdate()
        {
            UpdateCentreOfMassPivot(true);
        }

        public virtual void Import(Blueprint blueprint, ShipContext blockContext)
        {
            if (blueprint.Blocks.Controller == null)
            {
                throw new ArgumentException("Blueprint must have a controller reference");
            }

            blockGrid = new BlockGrid(pivot, this);

            // Initialization
            ActionScheduler = new ActionScheduler(this);
            Turrets = new TurretList();
            BlockContext = blockContext;

            var radius = blueprint.Blocks.MaxSize / 2f * canvasScaleMultiplier;
            mouseOverCollider.radius = radius;

            var template = blueprint.Blocks.Controller.BlueprintBlock.Template as ControllerTemplate;
            var controllerValues = template.controllerTemplatePartial.Convert();
            navigation = new ShipNavigation(this, navigationOptions, controllerValues.directionalForce);

            blockGrid.Import(blueprint, blockContext);
            this.blueprint = blueprint;

            UpdateCanvasSize(blueprint);
            UpdateCentreOfMassPivot(false);

            shipAI.Initialize();
        }

        public string GetInstanceString()
        {
            return $"{blueprint.name} : {gameObject.GetInstanceID()}";
        }

        public virtual void OnRaycastEnter()
        {
            overlay.overlayCircle.IsHovered = true;
            Systems.UI.mouseCursor.AddOverride(cursorOverride);

            if (Systems.IsDebugging(DebugMode.Ships))
            {
                Systems.UI.tooltips.shipAIDebugTooltip.Show(this);
            }
        }

        public virtual void OnRaycastExit()
        {
            overlay.overlayCircle.IsHovered = false;
            Systems.UI.mouseCursor.RemoveOverride(cursorOverride);

            if (Systems.IsDebugging(DebugMode.Ships))
            {
                Systems.UI.tooltips.shipAIDebugTooltip.Hide();
            }
        }

        public abstract ShipSelection GetAppropriateSelection(Formation formation);

        public abstract bool MatchesSelection(ShipSelection selection);

        private void UpdateCentreOfMassPivot(bool updateSelf)
        {
            var COMOffset = -blockGrid.CentreOfMass.GetCentreOfMass();

            if (updateSelf)
            {
                var currentPosition = pivot.localPosition.ToVector2();
                var diff = currentPosition - COMOffset;
                transform.localPosition += diff.ToVector3();
            }

            pivot.localPosition = COMOffset;
        }

        private void UpdateCanvasSize(Blueprint blueprint)
        {
            var size = blueprint.Blocks.MaxSize * 10 * canvasScaleMultiplier;
            overlay.rectContainer.sizeDelta = new Vector2(size, size);
        }

        public Tooltip GetTooltip()
        {
            return debugTooltip;
        }

        private TooltipGroup GetDebugTooltipComponents() => new TooltipGroup(new ITooltipComponent[]
        {
            new TooltipTitle(GetInstanceString(), false),
            new TooltipSpacer(),
            new TooltipText("Blueprint:"),
            new TooltipGroup(blueprint.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("BlockGrid:"),
            new TooltipGroup(blockGrid.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("State:"),
            new TooltipGroup(state.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("AI:"),
            new TooltipGroup(shipAI.GetDebugTooltipComponents(), 1)
        });
    }
}