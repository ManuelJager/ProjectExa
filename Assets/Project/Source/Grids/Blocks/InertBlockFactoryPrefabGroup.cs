using Exa.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public enum BlockTemplatePrefabType
    {
        inert,
        alive
    }

    public class InertBlockFactoryPrefabGroup : MonoBehaviour
    {
        protected Dictionary<string, IPool<PoolMember>> poolById = new Dictionary<string, IPool<PoolMember>>();

        [SerializeField] private PoolSettings defaultPoolSettings;

        protected virtual BlockTemplatePrefabType PrefabType => BlockTemplatePrefabType.inert; 

        /// <summary>
        /// Creates an inert block prefab on this group
        /// </summary> 
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateInertPrefab(BlockTemplate blockTemplate)
        {
            var id = blockTemplate.id;
            var prefab = CreatePrefab(blockTemplate, PrefabType);
            var pool = CreatePool<Pool>(prefab, $"Inert block pool: {id}", out var settings);
            poolById[id] = pool;
            pool.Configure(settings);
        }

        public GameObject GetBlock(string id, Transform parent)
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
        protected GameObject CreatePrefab(BlockTemplate blockTemplate, BlockTemplatePrefabType prefabType)
        {
            var prefab = GetBasePrefab(blockTemplate, prefabType);
            var instance = Instantiate(prefab, transform);
            instance.name = $"{blockTemplate.displayId}";

            // Set the sprite for the sprite renderer
            if (prefabType == BlockTemplatePrefabType.inert)
            {
                instance.GetComponent<SpriteRenderer>().sprite = blockTemplate.thumbnail;
            }

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

        private GameObject GetBasePrefab(BlockTemplate blockTemplate, BlockTemplatePrefabType prefabType)
        {
            switch (prefabType)
            {
                case BlockTemplatePrefabType.inert:
                    return blockTemplate.inertPrefab;

                case BlockTemplatePrefabType.alive:
                    return blockTemplate.alivePrefab;

                default:
                    return null;
            }
        }
    }
}