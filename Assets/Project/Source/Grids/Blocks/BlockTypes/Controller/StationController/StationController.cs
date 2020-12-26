using Exa.Grids.Blocks.Components;
using Exa.Ships.Targeting;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public abstract class StationController<T> : GenericController<StationControllerData>, ITurret, IBehaviourMarker<T>
        where T : struct, ITurretValues
    {
        [SerializeField] private TurretBehaviour<T> turretBehaviour;

        public void SetTarget(IWeaponTarget target) {
            turretBehaviour.Target = target;
        }

        BlockBehaviour<T> IBehaviourMarker<T>.Component => turretBehaviour;
    }
}