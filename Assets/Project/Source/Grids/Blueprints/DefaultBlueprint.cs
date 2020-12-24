using Exa.IO;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [CreateAssetMenu(menuName = "Grids/Blueprints/DefaultBlueprint")]
    public class DefaultBlueprint : ScriptableObject
    {
        [TextArea(3, 3000)] public string blueprintJson;

        public BlueprintContainer ToContainer() {
            var blueprint = IOUtils.JsonDeserializeWithSettings<Blueprint>(blueprintJson, SerializationMode.Readable);

            var args = new BlueprintContainerArgs(blueprint) {
                generateBlueprintFileHandle = false,
                useDefaultThumbnailFolder = true
            };

            var container = new BlueprintContainer(args);
            container.LoadThumbnail();
            return container;
        }
    }
}