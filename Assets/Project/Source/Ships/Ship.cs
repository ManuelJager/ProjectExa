using Exa.AI;
using Exa.Debugging;
using Exa.Gameplay;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Ships.Navigations;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Ships
{
    public abstract class Ship : MonoBehaviour, IRaycastTarget
    {
        [Header("References")]
        public ShipAI shipAI;
        public ShipState state;
        public new Rigidbody rigidbody;
        public Transform pivot;

        [Header("Settings")]
        public float canvasScaleMultiplier = 1f;
        public NavigationOptions navigationOptions;

        [HideInInspector] public ShipOverlay overlay;
        public ShipNavigation navigation;
        public BlockGrid blockGrid;
        protected Blueprint blueprint;

        [Header("Events")]
        public UnityEvent destroyEvent = new UnityEvent();

        public Blueprint Blueprint => blueprint;
        public ActionScheduler ActionScheduler { get; private set; }
        public Controller Controller { get; internal set; }

        private void FixedUpdate()
        {
            navigation?.ScheduledFixedUpdate();
            ActionScheduler.ExecuteActions(Time.fixedDeltaTime);
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

            // Initialization
            ActionScheduler = new ActionScheduler(this);
            blockGrid = new BlockGrid(pivot, this);

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

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetInstanceString());
            sb.AppendLine("------------------------------------------------------------");
            sb.AppendLine("Blueprint:");
            sb.AppendLine(blueprint.ToString(tabs + 1));
            sb.AppendLine("BlockGrid:");
            sb.AppendLine(blockGrid.ToString(tabs + 1));
            sb.AppendLine("State:");
            sb.AppendLine(state.ToString(tabs + 1));
            sb.AppendLine("AI:");
            sb.AppendLine(shipAI.ToString(tabs + 1));
            return sb.ToString();
        }
    }
}