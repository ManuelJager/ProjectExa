namespace Exa.Grids.Blocks.Components
{
    public interface IPowerGeneratorBehaviour
    {
        PowerGeneratorBehaviour PowerGeneratorBehaviour { get; }
    }

    public class PowerGeneratorBehaviour : BlockBehaviour<PowerGeneratorData>
    {
    }
}