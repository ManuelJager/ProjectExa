using Exa.Validation;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlueprintGridHasDisconnectedBlocks : ValidationError
    {
        public BlueprintGridHasDisconnectedBlocks(string message)
            : base(message)
        {
        }
    }
}