using Exa.Pooling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public abstract class BlockPoolGroupBase : MonoBehaviour
    {
        protected Dictionary<string, IPool<PoolMember>> poolById = new Dictionary<string, IPool<PoolMember>>();

        [SerializeField] private PoolSettings defaultPoolSettings;

        protected abstract PrefabType PrefabType { get; }

        public GameObject GetInactiveBlock(string id, Transform parent)
        {
            var blockGO = poolById[id].Retrieve().gameObject;

            blockGO.transform.SetParent(parent);

            return blockGO;
        }

        /// <summary>
        /// Creates a block prefab for the given block template on this group
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <param name="inert"></param>
        /// <returns></returns>
        protected GameObject CreatePrefab(BlockTemplate blockTemplate, PrefabType prefabType)
        {
            var prefab = GetBasePrefab(blockTemplate, prefabType);
            var instance = Instantiate(prefab, transform);
            instance.name = $"{blockTemplate.displayId}";

            return instance;
        }

        protected T CreatePool<T>(GameObject prefab, string name, out PoolSettings settings)
            where T : Component, IPool<PoolMember>
        {
            var poolGO = new GameObject(name);
            poolGO.transform.SetParent(transform);

            var pool = poolGO.AddComponent<T>();

            settings = defaultPoolSettings.Clone();
            settings.prefab = prefab;

            return pool;
        }

        private GameObject GetBasePrefab(BlockTemplate blockTemplate, PrefabType prefabType)
        {
            switch (prefabType)
            {
                case PrefabType.inert:
                    return blockTemplate.inertPrefab;

                case PrefabType.alive:
                    return blockTemplate.alivePrefab;

                default:
                    return null;
            }
        }
    }
}
