using Exa.Gameplay;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids.Ships
{
    public class FriendlyShip : Ship, IRaycastTarget
    {
        [SerializeField] private CircleCollider2D mouseOverCollider;

        public override void Import(Blueprint blueprint)
        {
            base.Import(blueprint);
            
            var radius = blueprint.Blocks.MaxSize / 2f;
            mouseOverCollider.radius = radius;
        }

        public void OnRaycastEnter()
        {
            Systems.MainUI.mouseCursor.SetState(UI.CursorState.active);
        }

        public void OnRaycastExit()
        {
            Systems.MainUI.mouseCursor.SetState(UI.CursorState.idle);
        }
    }
}