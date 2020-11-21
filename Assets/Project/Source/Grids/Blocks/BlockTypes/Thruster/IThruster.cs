using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public interface IThruster : IBehaviourMarker<ThrusterData>
    {
        void Fire(float strength);
        void PowerDown();
    }
}