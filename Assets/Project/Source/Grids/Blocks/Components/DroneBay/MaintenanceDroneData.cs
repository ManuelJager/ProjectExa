using System;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct MaintenanceDroneData {
        public float hull;
        public float armor;
        public float repairSpeed; // As in hull per second repaired
    }
}