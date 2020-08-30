using Exa.AI;
using Exa.Debugging;
using Exa.Gameplay;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Ships.Navigations;
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
        public ShipAI shipAI;
        public ShipState state;
        public new Rigidbody rigidbody;
        public Transform pivot;
        public NavigationOptions navigationOptions;

        [Header("Settings")]
        public float canvasScaleMultiplier = 1f;
        public Font shipDebugFont;

        [HideInInspector] public ShipOverlay overlay;
        public ShipNavigation navigation;
        public BlockGrid blockGrid;
        protected Blueprint blueprint;
        private Tooltip debugTooltip;

        [Header("Events")]
        public UnityEvent destroyEvent = new UnityEvent();

        public Blueprint Blueprint => blueprint;
        public ActionScheduler ActionScheduler { get; private set; }
        public Controller Controller { get; internal set; }
        public TurretList Turrets { get; private set; }
        public BlockContext BlockContext { get; private set; }

        protected virtual void Awake()
        {
            debugTooltip = new Tooltip(GetDebugTooltipComponents, shipDebugFont);
        }

        private void FixedUpdate()
        {
            navigation?.ScheduledFixedUpdate();
            ActionScheduler.ExecuteActions(Time.fixedDeltaTime);

            if (debugTooltip != null)
            {
                debugTooltip.IsDirty = true;
            }
        }

        private void LateUpdate()
        {
            UpdateCentreOfMassPivot(true);
        }

        public virtual void Import(Blueprint blueprint, BlockContext blockContext)
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

            if (Systems.IsDebugging(DebugMode.Ships))
            {
                Systems.UI.tooltips.shipAIDebugTooltip.Show(this);
            }
        }

        public virtual void OnRaycastExit()
        {
            overlay.overlayCircle.IsHovered = false;

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

        private TooltipContainer GetDebugTooltipComponents() => new TooltipContainer(new ITooltipComponent[]
        {
            new TooltipTitle(GetInstanceString(), false),
            new TooltipSpacer(),
            new TooltipText("Blueprint:"),
            new TooltipContainer(blueprint.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("BlockGrid:"),
            new TooltipContainer(blockGrid.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("State:"),
            new TooltipContainer(state.GetDebugTooltipComponents(), 1),
            new TooltipSpacer(),
            new TooltipText("AI:"),
            new TooltipContainer(shipAI.GetDebugTooltipComponents(), 1)
        });
    }
}