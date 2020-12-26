using System;

namespace Exa.Grids.Blocks
{
    [Flags]
    public enum BlockCategory
    {
        Armor = 1 << 0,
        ShipController = 1 << 1,
        Thruster = 1 << 2,
        Gyroscope = 1 << 3,
        Power = 1 << 4,
        Worker = 1 << 5,
        Weapon = 1 << 6,
        StationController = 1 << 7
    }
}