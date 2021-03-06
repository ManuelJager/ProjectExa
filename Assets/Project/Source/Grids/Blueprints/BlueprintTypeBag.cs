﻿using Exa.Generics;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [CreateAssetMenu(menuName = "Grids/Blueprints/BlueprintTypes")]
    public class BlueprintTypeBag : ScriptableObjectBag<BlueprintType>
    {
        private class BlueprintTypeComparer : IComparer<BlueprintType>
        {
            public int Compare(BlueprintType x, BlueprintType y) {
                return GetBlueprintTypeSize(x) - GetBlueprintTypeSize(y);
            }

            private static int GetBlueprintTypeSize(BlueprintType blueprintType) {
                return blueprintType.maxSize.x * blueprintType.maxSize.y;
            }
        }

        public Dictionary<BlueprintTypeGuid, BlueprintType> typesById;

        public override void FindObjects() {
            base.FindObjects();
            objects.Sort(new BlueprintTypeComparer());
        }

        public void OnEnable() {
            typesById = new Dictionary<BlueprintTypeGuid, BlueprintType>();
            foreach (var type in objects)
                typesById[type.typeGuid] = type;
        }
    }
}