using Exa.Validation;

namespace Exa.Grids.Blueprints
{
    public class BlueprintNameDefaultError : ValidationError
    {
        public BlueprintNameDefaultError(string message)
            : base(message)
        {
        }
    }
}