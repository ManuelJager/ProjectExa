using Exa.Grids.Blocks.BlockTypes;
using Exa.Math;
using System.Collections.Generic;

namespace Exa.Ships
{
    public class ThrusterGroup : List<Thruster>
    {
        public void Fire(float force)
        {
            // NOTE: This whole thing is scuffed. Please fix
            // TODO: Use the amount of force the group applies as a collective and devide it by the maximum group force.
            var strenth = force.Remap(0f, 50f, 0f, 1f);
            foreach (var item in this)
            {
                item.Fire(strenth);
            }
        }
    }
}