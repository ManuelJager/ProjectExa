using Exa.Bindings;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Observable container for block templates
    /// Used by the editor inventory to keep track of block template updates
    /// </summary>
    public class ObservableBlockTemplate : Observable<BlockTemplate>
    {
        public ObservableBlockTemplate(BlockTemplate data)
            : base(data)
        {
        }
    }
}