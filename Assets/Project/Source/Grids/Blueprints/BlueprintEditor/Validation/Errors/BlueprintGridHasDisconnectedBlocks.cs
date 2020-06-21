using Exa.Schemas;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlueprintGridHasDisconnectedBlocks : ValidationError
    {
        public BlueprintGridHasDisconnectedBlocks(string message)
            : base("BlueprintGridHasDisconnectedBlocks", message)
        {
        }
    }
}