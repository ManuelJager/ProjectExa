namespace Exa.Grids.Blocks.Components
{
    public interface ITurretValues : IBlockComponentValues
    {
        float TurningRate { get; } // in degrees rotation per second
        float FiringRate { get; } // in rounds per second
        float Damage { get; } // TODO: Expand the damage model
    }
}
