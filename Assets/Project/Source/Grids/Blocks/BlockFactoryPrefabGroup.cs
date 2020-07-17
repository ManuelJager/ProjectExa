using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public class BlockFactoryPrefabGroup : InertBlockFactoryPrefabGroup
    {
        protected override BlockTemplatePrefabType PrefabType => BlockTemplatePrefabType.alive;

        /// <summary>
        /// Creates an alive prefab on this group.
        /// <para>
        /// Either copies the current alive prefab on the tamplate, or generates a new alive prefab based on the inert prefab
        /// </para>
        /// </summary> 
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateAlivePrefab(BlockTemplate blockTemplate)
        {
            var instance = blockTemplate.GeneratePrefab
                ? GeneratePrefab(blockTemplate)
                : CreatePrefab(blockTemplate, PrefabType).GetComponent<Block>();

            try
            {
                blockTemplate.SetValues(instance);
            }
            catch (Exception e)
            {
                throw new Exception($"Error on setting value for block template with id: {blockTemplate.id}", e);
            }

            var id = blockTemplate.id;
            poolById[id] = CreatePool(instance.gameObject, $"Block pool: {id}");
        }

        /// <summary>
        /// Generates a prefab for a given block template using the inert prefab as a base
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        private Block GeneratePrefab(BlockTemplate blockTemplate)
        {
            var instance = CreatePrefab(blockTemplate, BlockTemplatePrefabType.inert);
            instance.GetComponent<SpriteRenderer>().sprite = blockTemplate.thumbnail;
            return blockTemplate.AddBlockOnGameObject(instance);
        }
    }
}