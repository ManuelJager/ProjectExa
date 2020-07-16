using Exa.Grids.Blocks.BlockTypes;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public class BlockFactoryPrefabGroup : InertBlockFactoryPrefabGroup
    {
        /// <summary>
        /// Creates an alive prefab on this group.
        /// <para>
        /// Either copies the current alive prefab on the tamplate, or generates a new alive prefab based on the inert prefab
        /// </para>
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public override GameObject CreatePrefab(BlockTemplate blockTemplate)
        {
            var instance = blockTemplate.GeneratePrefab
                ? GeneratePrefab(blockTemplate)
                : CreatePrefab(blockTemplate, BlockTemplatePrefabType.alive).GetComponent<Block>();

            try
            {
                blockTemplate.SetValues(instance);
            }
            catch (Exception e)
            {
                throw new Exception($"Error on setting value for block template with id: {blockTemplate.id}", e);
            }

            return instance.gameObject;
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