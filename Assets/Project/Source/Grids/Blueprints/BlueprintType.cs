using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    [CreateAssetMenu(fileName = "BlueprintType", menuName = "Grids/Blueprints/BlueprintType")]
    public class BlueprintType : ScriptableObject
    {
        public string displayName;
        public string typeGuid;
        public Vector2Int maxSize;
        public List<string> disallowedBlockCategories;
    }
}