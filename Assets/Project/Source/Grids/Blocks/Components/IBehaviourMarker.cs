using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks
{
    public interface IBehaviourMarker<T>
        where T : struct, IBlockComponentValues
    {
        BlockBehaviour<T> Component { get; }
    }
}