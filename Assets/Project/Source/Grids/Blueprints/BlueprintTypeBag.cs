using System;
using Exa.Generics;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [CreateAssetMenu(menuName = "Grids/Blueprints/BlueprintTypes")]
    public class BlueprintTypeBag : ScriptableObjectBag<BlueprintType>
    {
        private class BlueprintTypeComparer : IComparer<BlueprintType>
        {
            public int Compare(BlueprintType x, BlueprintType y)
            {
                return GetBlueprintTypeSize(x) - GetBlueprintTypeSize(y);
            }

            private static int GetBlueprintTypeSize(BlueprintType blueprintType)
            {
                return blueprintType.maxSize.x * blueprintType.maxSize.y;
            }
        }

        public Dictionary<string, BlueprintType> typesById;

        public override void FindObjects()
        {
            base.FindObjects();
            objects.Sort(new BlueprintTypeComparer());
        }

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