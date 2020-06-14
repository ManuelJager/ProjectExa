using Exa.Bindings;
using Exa.IO;
using Newtonsoft.Json;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(ObservableBlueprintConverter))]
    public class ObservableBlueprint : Observable<Blueprint>, ISerializationCollectionItem
    {
        public ObservableBlueprint(Blueprint blueprint)
            : base(blueprint)
        {
        }

        public string itemName => Data.name;
    }
}