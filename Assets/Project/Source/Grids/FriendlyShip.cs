using Exa.Gameplay;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public class FriendlyShip : Ship, IRaycastTarget
    {
        [SerializeField] private CircleCollider2D mouseOverCollider;

        public override void Import(Blueprint blueprint)
        {
            base.Import(blueprint);
            
            var bpSize = blueprint.Blocks.Size.Value;
            var radius = Mathf.Max(bpSize.x, bpSize.y) / 2f;
            mouseOverCollider.radius = radius;
        }

        public void OnRaycastEnter()
        {
            Systems.MainUI.mousePointer.SetState(UI.CursorState.active);
        }

        public void OnRaycastExit()
        {
            Systems.MainUI.mousePointer.SetState(UI.CursorState.idle);
        }
    }
}