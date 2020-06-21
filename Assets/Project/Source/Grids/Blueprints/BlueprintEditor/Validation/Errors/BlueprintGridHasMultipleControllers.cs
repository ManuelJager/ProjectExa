using Exa.Schemas;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlueprintGridHasMultipleControllers : ValidationError
    {
        public BlueprintGridHasMultipleControllers(string message)
            : base("BlueprintGridHasMultipleControllers", message)
        {
        }
    }
}