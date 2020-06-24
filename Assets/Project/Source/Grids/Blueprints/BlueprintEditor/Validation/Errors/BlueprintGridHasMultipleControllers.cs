using Exa.Validation;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlueprintGridHasMultipleControllers : ValidationError
    {
        public BlueprintGridHasMultipleControllers(string message)
            : base(message)
        {
        }
    }
}