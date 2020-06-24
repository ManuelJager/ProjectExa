using Exa.Validation;

namespace Exa.Grids.Blueprints
{
    public class BlueprintNameDuplicateError : ValidationError
    {
        public BlueprintNameDuplicateError(string message)
            : base(message)
        {
        }
    }
}