using Exa.Gameplay;
using Exa.Grids.Blueprints;
using Exa.UI;
using UnityEngine;

namespace Exa.Grids.Ships
{
    public class FriendlyShip : Ship
    {
        [SerializeField] private CircleCollider2D mouseOverCollider;

        private static CursorOverride cursorOverride;

        protected override void Awake()
        {
            base.Awake();
            cursorOverride = new CursorOverride(CursorState.active, this);
        }

        public override void Import(Blueprint blueprint)
        {
            base.Import(blueprint);
            
            var radius = blueprint.Blocks.MaxSize / 2f * references.canvasScaleMultiplier;
            mouseOverCollider.radius = radius;
        }

        public override void OnRaycastEnter()
        {
            base.OnRaycastEnter();
            Systems.MainUI.mouseCursor.AddOverride(cursorOverride);
        }

        public override void OnRaycastExit()
        {
            base.OnRaycastExit();
            Systems.MainUI.mouseCursor.RemoveOverride(cursorOverride);
        }

        public override ShipSelection GetAppropriateSelection()
        {
            return new FriendlyShipSelection();
        }

        public override bool MatchesSelection(ShipSelection selection)
        {
            return selection is FriendlyShipSelection;
        }
    }
}