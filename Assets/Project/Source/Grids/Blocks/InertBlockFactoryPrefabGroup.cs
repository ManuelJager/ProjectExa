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
        protected Dictionary<string, GameObject> prefabById = new Dictionary<string, GameObject>();

        /// <summary>
        /// Creates an inert block prefab on this group
        /// </summary> 
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public virtual GameObject CreatePrefab(BlockTemplate blockTemplate)
        {
            return CreatePrefab(blockTemplate, BlockTemplatePrefabType.inert);
        }

        public GameObject InstantiateBlock(string id)
        {
            return Instantiate(prefabById[id]);
        }

        public GameObject InstantiateBlock(string id, Transform parent)
        {
            return Instantiate(prefabById[id], parent);
        }

        /// <summary>
        /// Creates a block prefab for the given block template on this group
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <param name="inert"></param>
        /// <returns></returns>
        protected GameObject CreatePrefab(BlockTemplate blockTemplate, BlockTemplatePrefabType prefabType)
        {
            var prefab = GetPrefab(blockTemplate, prefabType);
            var instance = Instantiate(prefab, transform);
            instance.name = $"{blockTemplate.displayId}";

            // Set the sprite for the sprite renderer
            if (prefabType == BlockTemplatePrefabType.inert)
            {
                instance.GetComponent<SpriteRenderer>().sprite = blockTemplate.thumbnail;
            }

            prefabById[blockTemplate.id] = instance;
            return instance;
        }

        private GameObject GetPrefab(BlockTemplate blockTemplate, BlockTemplatePrefabType prefabType)
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