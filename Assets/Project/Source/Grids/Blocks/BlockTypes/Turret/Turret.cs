using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public interface ITurret : IBehaviourMarker<TurretData>
    {
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
    }
}