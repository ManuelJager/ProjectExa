using Exa.Grids.Blocks.Components;
using Exa.Ships.Targetting;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public interface ITurret : IBehaviourMarker<TurretData>
    {
        void SetTarget(ITarget target);
    }

    public class Turret : Block, ITurret
    {
        [SerializeField] private TurretBehaviour turretBehaviour;

        BlockBehaviour<TurretData> IBehaviourMarker<TurretData>.Component => turretBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(turretBehaviour);
        }

        public void SetTarget(ITarget target)
        {
            turretBehaviour.Target = target;
        }

        protected override void OnAdd()
        {
            Ship.Turrets.Add(this);
        }

        protected override void OnRemove()
        {
            Ship.Turrets.Remove(this);
        }
    }
}