using Exa.Validation;

namespace Exa.Grids.Blueprints.Editor
{
    public class BlueprintGridHasDisconnectedBlocks : ValidationError
    {
        public BlueprintGridHasDisconnectedBlocks(string message)
            : base(message)
        {
        }
    }
}