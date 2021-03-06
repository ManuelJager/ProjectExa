﻿using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships.Targeting;
using System.Collections.Generic;

namespace Exa.Ships
{
    public class TurretList : List<ITurret>
    {
        public void SetTarget(IWeaponTarget target) {
            foreach (var turret in this) {
                turret.SetTarget(target);
            }
        }
    }
}