using Exa.Validation;

namespace Exa.Grids.Blueprints.Editor
{
    public class BlueprintNameEmptyError : ValidationError
    {
        public BlueprintNameEmptyError(string message)
            : base(message)
        {
        }
    }
}