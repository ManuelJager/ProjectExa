using Exa.Generics;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [CreateAssetMenu(menuName = "Grids/Blueprints/BlueprintTypes")]
    public class BlueprintTypeBag : ScriptableObjectBag<BlueprintType>
    {
        public Dictionary<string, BlueprintType> typesById;

        public void OnEnable()
        {
            typesById = new Dictionary<string, BlueprintType>();
            foreach (var type in objects)
            {
                typesById[type.typeGuid] = type;
            }
        }
    }
}