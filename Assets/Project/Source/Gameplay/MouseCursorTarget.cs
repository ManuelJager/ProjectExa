using Exa.Ships.Targeting;
using UnityEngine;

namespace Exa.Gameplay
{
    public class MouseCursorTarget : IWeaponTarget
    {
        public Vector2 GetPosition(Vector2 current) {
            return Systems.Input.MouseWorldPoint;
        }

        public bool GetTargetValid() {
            return true;
        }
    }
}