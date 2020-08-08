namespace Exa.Grids.Blocks.Components
{
    public interface IGyroscope
    {
        GyroscopeBehaviour GyroscopeBehaviour { get; }
    }

    public class GyroscopeBehaviour : BlockBehaviour<GyroscopeData>
    {
    }
}