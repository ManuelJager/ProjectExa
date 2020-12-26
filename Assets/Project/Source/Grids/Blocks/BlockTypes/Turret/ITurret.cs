using Exa.Grids.Blocks.Components;
using Exa.Ships.Targeting;

namespace Exa.Grids.Blocks.BlockTypes
{
    public interface ITurret
    {
        void SetTarget(IWeaponTarget target);
    }
}