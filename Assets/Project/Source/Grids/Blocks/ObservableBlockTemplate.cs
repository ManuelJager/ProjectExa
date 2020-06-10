using Exa.Bindings;

namespace Exa.Grids.Blocks
{
    public class ObservableBlockTemplate : Observable<BlockTemplate>
    {
        public ObservableBlockTemplate(BlockTemplate data)
            : base(data)
        {
        }
    }
}