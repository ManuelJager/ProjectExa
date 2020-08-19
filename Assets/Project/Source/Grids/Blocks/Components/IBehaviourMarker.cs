using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks
{
    public interface IBehaviourMarker<T>
        where T : IBlockComponentData
    {
        BlockBehaviour<T> Component { get; set; }
    }
}