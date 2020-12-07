using UnityEngine;

namespace Exa.Utils
{
    public static class ObjectExtensions
    {
        public static GameObject Instantiate(this Object obj, GameObject prefab, Transform parent, string namePrefix = null) {
            var go = Object.Instantiate(prefab, parent);
            go.name = $"{namePrefix}: {go.GetInstanceID()}";
            return go;
        }

        public static T InstantiateAndGet<T>(this Object obj, GameObject prefab, Transform parent, string namePrefix = null) {
            var go = Instantiate(obj, prefab, parent, namePrefix ?? typeof(T).Name);
            return go.GetComponent<T>();
        }
    }
}