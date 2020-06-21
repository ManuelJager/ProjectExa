using Exa.Schemas;

namespace Exa.Grids.Blueprints
{
    public class BlueprintNameDefaultError : ValidationError
    {
        public BlueprintNameDefaultError(string message)
            : base("BlueprintNameDefaultError", message)
        {
        }
    }
}