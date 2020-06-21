using Exa.Schemas;

namespace Exa.Grids.Blueprints
{
    public class BlueprintNameEmptyError : ValidationError
    {
        public BlueprintNameEmptyError(string message)
            : base("BlueprintNameEmptyError", message)
        {
        }
    }
}