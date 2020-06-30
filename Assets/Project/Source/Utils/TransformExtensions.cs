using System.Collections.Generic;
using UnityEngine;

namespace Exa.Utils
{
    public static class TransformExtensions
    {
        public static IEnumerable<Transform> GetChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                yield return child;
            }
        }
    }
}