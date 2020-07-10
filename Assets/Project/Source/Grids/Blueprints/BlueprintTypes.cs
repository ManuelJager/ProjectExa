using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [CreateAssetMenu(fileName = "BlueprintTypes", menuName = "Grids/Blueprints/BlueprintTypes")]
    public class BlueprintTypes : ScriptableObject
    {
        public Dictionary<string, BlueprintType> typesById;
        public List<BlueprintType> types;

        public void OnEnable()
        {
            typesById = new Dictionary<string, BlueprintType>();
            foreach (var type in types)
            {
                typesById[type.typeGuid] = type;
            }
        }
    }
}