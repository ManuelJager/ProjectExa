using Exa.Schemas;

namespace Exa.Grids.Blueprints
{
    public class BlueprintNameDuplicateError : ValidationError
    {
        public BlueprintNameDuplicateError(string message)
            : base("BlueprintNameDuplicateError", message)
        {
        }
    }
}