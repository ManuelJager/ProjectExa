using System.Linq;
using Exa.Bindings;
using Exa.Grids.Blueprints;

namespace Exa.UI
{
    public class FleetBuilderBlueprintViewController : ViewController<FleetBuilderBlueprintView, BlueprintContainer, Blueprint>
    {
        public void SetFilter(BlueprintType blueprintType)
        {
            foreach (var obj in views.Select(kvp => new
            {
                value = kvp.Key.Data,
                view = kvp.Value
            }))
            {
                var activeSelf = obj.value.BlueprintType == blueprintType && obj.view.Selected;
                obj.view.gameObject.SetActive(activeSelf);
            }
        }
    }
}