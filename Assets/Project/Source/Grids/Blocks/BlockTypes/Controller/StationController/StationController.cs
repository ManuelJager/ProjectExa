using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using Exa.Ships.Targeting;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public abstract class StationController<T> : GenericController<StationControllerData>, IBehaviourMarker<T>
        where T : struct, ITurretValues
    {
        [SerializeField] protected TurretBehaviour<T> turretBehaviour;

        public bool AutoFireEnabled => false;

        public void SetTarget(IWeaponTarget target) {
            turretBehaviour.Target = target;
        }

        public void Fire() {
            turretBehaviour.Fire();
        }

        BlockBehaviour<T> IBehaviourMarker<T>.Component => turretBehaviour;

        public override IEnumerable<BlockBehaviour> GetBehaviours() {
            return base.GetBehaviours()
                .Append(turretBehaviour);
        }
    }
}