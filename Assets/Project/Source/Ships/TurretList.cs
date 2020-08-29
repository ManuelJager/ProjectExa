using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships.Targetting;
using System.Collections.Generic;

namespace Exa.Ships
{
    public class TurretList : List<ITurret>
    {
        public void SetTarget(ITarget target)
        {
            foreach (var turret in this)
            {
                turret.SetTarget(target);
            }
        }
    }
}