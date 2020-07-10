using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [CreateAssetMenu(menuName = "Grids/Blueprints/DefaultBlueprint")]
    public class DefaultBlueprint : ScriptableObject
    {
        [TextArea] public string blueprintJson;
    }
}