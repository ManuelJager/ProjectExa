using Exa.Bindings;
using Newtonsoft.Json;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(ObservableBlueprintConverter))]
    public class ObservableBlueprint : Observable<Blueprint>
    {
        public ObservableBlueprint(Blueprint blueprint)
            : base(blueprint)
        {
        }
    }
}