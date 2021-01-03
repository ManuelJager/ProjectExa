using Exa.Grids.Blocks.BlockTypes;

namespace Exa.Grids.Blocks.Components
{
    public interface IChargeableTurretPlatform : ITurretPlatform
    {
        void StartCharge();
        void EndChange();
    }
}