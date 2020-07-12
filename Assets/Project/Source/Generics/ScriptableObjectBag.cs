using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Exa.Generics
{
    public class ScriptableObjectBag<T> : ScriptableObjectBagBase, IEnumerable<T>
        where T : ScriptableObject
    {
        public List<T> objects = new List<T>();

        public override void FindObjects()
        {
            objects = GetAllInstances();
        }

        private List<T> GetAllInstances()
        {
            var guids = QueryGUIDs();
            var collection = new List<T>(guids.Length);

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                collection.Add(AssetDatabase.LoadAssetAtPath<T>(path));
            }

            return collection;
        }

        protected virtual string[] QueryGUIDs()
        {
            return AssetDatabase.FindAssets("t:" + typeof(T).Name);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return objects.GetEnumerator();
        }
    }

    public abstract class ScriptableObjectBagBase : ScriptableObject
    {
        public abstract void FindObjects();
    }
}