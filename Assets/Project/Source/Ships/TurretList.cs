using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships.Targeting;
using System.Collections.Generic;

namespace Exa.Ships
{
    public class TurretList : List<ITurretPlatform>
    {
        public void SetTarget(IWeaponTarget target) {
            foreach (var turret in this) {
                if (turret.AutoFireEnabled) {
                    turret.SetTarget(target);
                }
            }
        }
    }
}