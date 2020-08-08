using Exa.Gameplay;
using Exa.Grids.Blueprints;
using Exa.UI;
using UnityEngine;

namespace Exa.Grids.Ships
{
    public class FriendlyShip : Ship, IRaycastTarget
    {
        [SerializeField] private CircleCollider2D mouseOverCollider;

        private static CursorOverride cursorOverride = new CursorOverride(CursorState.active);

        public override void Import(Blueprint blueprint)
        {
            base.Import(blueprint);
            
            var radius = blueprint.Blocks.MaxSize / 2f + 12;
            mouseOverCollider.radius = radius;
        }

        public void OnRaycastEnter()
        {
            Systems.MainUI.mouseCursor.AddOverride(cursorOverride);
        }

        public void OnRaycastExit()
        {
            Systems.MainUI.mouseCursor.RemoveOverride(cursorOverride);
        }
    }
}