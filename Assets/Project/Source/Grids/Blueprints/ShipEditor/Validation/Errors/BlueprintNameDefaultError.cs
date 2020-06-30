using Exa.Validation;

namespace Exa.Grids.Blueprints.Editor
{
    public class BlueprintNameDefaultError : ValidationError
    {
        public BlueprintNameDefaultError(string message)
            : base(message)
        {
        }
    }
}