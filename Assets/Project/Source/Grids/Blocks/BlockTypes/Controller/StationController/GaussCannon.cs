using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class GaussCannon : StationController<GaussCannonData>, IChargeableTurretPlatform
    {
        public void StartCharge() {
            ((GaussCannonBehaviour)turretBehaviour).StartCharge();
        }

        public void EndChange() {
            ((GaussCannonBehaviour)turretBehaviour).EndCharge();
        }
    }
}