using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class AutoFireTurret<T> : TurretBehaviour<T>, IAutoFireTurret
        where T : struct, ITurretValues
    {
        public virtual bool AutoFire { get; set; } = true;
        
        protected override void BlockUpdate() {
            base.BlockUpdate();
            AttemptFire();           
        }

        protected virtual void AttemptFire() {
            if (Target == null || !AutoFire)
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