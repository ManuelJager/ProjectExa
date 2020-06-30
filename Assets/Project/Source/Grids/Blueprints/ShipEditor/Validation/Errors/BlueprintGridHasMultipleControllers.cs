using Exa.Validation;

namespace Exa.Grids.Blueprints.Editor
{
    public class BlueprintGridHasMultipleControllers : ValidationError
    {
        public BlueprintGridHasMultipleControllers(string message)
            : base(message)
        {
        }
    }
}