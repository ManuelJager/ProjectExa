using Exa.Validation;

namespace Exa.Grids.Blueprints.Editor
{
    public class BlueprintNameDuplicateError : ValidationError
    {
        public BlueprintNameDuplicateError(string message)
            : base(message)
        {
        }
    }
}