namespace Exa.Grids.Blocks.Components
{
    public interface IPowerGenerator
    {
        PowerGeneratorBehaviour PowerGeneratorBehaviour { get; }
    }

    public class PowerGeneratorBehaviour : BlockBehaviour<PowerGeneratorData>
    {
    }
}