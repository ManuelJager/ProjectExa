using Exa.IO;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [CreateAssetMenu(menuName = "Grids/Blueprints/DefaultBlueprint")]
    public class DefaultBlueprint : ScriptableObject
    {
        [TextArea] public string blueprintJson;

        public BlueprintContainer ToContainer()
        {
            var blueprint = IOUtils.JsonDeserializeWithSettings<Blueprint>(blueprintJson, SerializationMode.readable);
            return new BlueprintContainer(blueprint, false);
        }
    }
}