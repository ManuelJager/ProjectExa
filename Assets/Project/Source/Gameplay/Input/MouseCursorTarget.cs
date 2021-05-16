using Exa.Ships.Targeting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Gameplay
{
    public class MouseCursorTarget : IWeaponTarget
    {
        private UnityEngine.Camera camera;
        private bool useDefault;

        public MouseCursorTarget() {
            this.camera = null;
            this.useDefault = true;
        }
        
        public MouseCursorTarget(UnityEngine.Camera camera) {
            this.camera = camera;
            this.useDefault = false;
        }
        
        public Vector2 GetPosition(Vector2 current) {
            return useDefault 
                ? Systems.Input.MouseWorldPoint 
                : (Vector2) camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        public bool GetTargetValid() {
            return true;
        }
    }
}