using Exa.Validation;

namespace Exa.Grids.Blueprints
{
    public class BlueprintNameEmptyError : ValidationError
    {
        public BlueprintNameEmptyError(string message)
            : base(message)
        {
        }
    }
}