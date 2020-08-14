using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks
{
    public interface IBehaviourMarker<S>
        where S : IBlockComponentData
    {
        BlockBehaviour<S> Component { get; set; }
    }
}