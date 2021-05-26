using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class AutoFireTurret<T> : TurretBehaviour<T>
        where T : struct, ITurretValues
    {
        private void Awake() {
            AutoFireEnabled = true;
        }
        
        protected override void BlockUpdate() {
            base.BlockUpdate();
            AttemptFire();           
        }

        protected virtual void AttemptFire() {
            if (Target == null || !AutoFireEnabled)
                return;
            
            // Long debug line cause debugging in unity sucks
            // Debug.Log($"({block.GetInstanceString()}) Current angle: {GetCurrentAngle()}, Target angle: {SelectTargetAngle()}, Diff {Mathf.DeltaAngle(GetCurrentAngle(), SelectTargetAngle())} ({rotationResult.deltaToTarget})");

            if (Mathf.Abs(rotationResult.deltaToTarget) > 0.5f || timeSinceFire < data.FiringRate)
                return;

            timeSinceFire -= data.FiringRate; 
            Fire();
        }
    }
}