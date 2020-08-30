using Exa.Gameplay;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.UI;
using UnityEngine;

namespace Exa.Ships
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

        public override void Import(Blueprint blueprint, ShipContext blockContext)
        {
            base.Import(blueprint, blockContext);
            
            var radius = blueprint.Blocks.MaxSize / 2f * canvasScaleMultiplier;
            mouseOverCollider.radius = radius;
        }

        public override void OnRaycastEnter()
        {
            base.OnRaycastEnter();
            Systems.UI.mouseCursor.AddOverride(cursorOverride);
        }

        public override void OnRaycastExit()
        {
            base.OnRaycastExit();
            Systems.UI.mouseCursor.RemoveOverride(cursorOverride);
        }

        public override ShipSelection GetAppropriateSelection(Formation formation)
        {
            return new FriendlyShipSelection(formation);
        }

        public override bool MatchesSelection(ShipSelection selection)
        {
            return selection is FriendlyShipSelection;
        }
    }
}