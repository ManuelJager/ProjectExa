using Exa.Ships.Targeting;

namespace Exa.Grids.Blocks.BlockTypes
{
    public interface ITurretPlatform
    {
        bool AutoFireEnabled { get; }
        void SetTarget(IWeaponTarget target);
        void Fire();
    }
}