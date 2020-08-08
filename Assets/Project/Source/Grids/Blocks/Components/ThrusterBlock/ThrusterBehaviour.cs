namespace Exa.Grids.Blocks.Components
{
    public interface IThruster
    {
        ThrusterBehaviour ThrusterBehaviour { get; }
    }
    public class ThrusterBehaviour : BlockBehaviour<ThrusterData>
    {
    }
}