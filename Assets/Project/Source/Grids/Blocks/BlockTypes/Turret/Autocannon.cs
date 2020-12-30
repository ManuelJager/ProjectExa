using Exa.Grids.Blocks.Components;
using Exa.Ships.Targeting;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Autocannon : Block, IBehaviourMarker<AutocannonData>, ITurretPlatform
    {
        [SerializeField] private AutocannonBehaviour turretBehaviour;

        BlockBehaviour<AutocannonData> IBehaviourMarker<AutocannonData>.Component => turretBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours() {
            return base.GetBehaviours()
                .Append(turretBehaviour);
        }

        public bool AutoFireEnabled => turretBehaviour.AutoFireEnabled;

        public void SetTarget(IWeaponTarget target) {
            turretBehaviour.Target = target;
        }
    }
}