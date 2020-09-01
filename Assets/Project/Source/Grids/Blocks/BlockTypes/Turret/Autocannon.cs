﻿using Exa.Grids.Blocks.Components;
using Exa.Ships.Targetting;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Autocannon : Block, IBehaviourMarker<AutocannonData>, ITurret
    {
        [SerializeField] private AutocannonBehaviour turretBehaviour;

        BlockBehaviour<AutocannonData> IBehaviourMarker<AutocannonData>.Component => turretBehaviour;
        public ITurretValues Data => (this as IBehaviourMarker<AutocannonData>).Component.Data;

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