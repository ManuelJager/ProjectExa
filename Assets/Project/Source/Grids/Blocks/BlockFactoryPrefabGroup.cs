using Exa.Grids.Blocks.BlockTypes;
using System;

namespace Exa.Grids.Blocks
{
    public class BlockFactoryPrefabGroup : InertBlockFactoryPrefabGroup
    {
        protected override BlockTemplatePrefabType PrefabType => BlockTemplatePrefabType.alive;

        /// <summary>
        /// Creates an alive prefab on this group.
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateAlivePrefabGroup(BlockTemplate blockTemplate)
        {
            var rootInstanceGO = CreatePrefab(blockTemplate, PrefabType);
            var rootInstance = rootInstanceGO.GetComponent<Block>();

            foreach (var component in rootInstance.GetBehaviours())
            {
                component.block = rootInstance;
            }

            //try
            //{
            //    blockTemplate.SetValues(rootInstance);
            //}
            //catch (Exception e)
            //{
            //    throw new Exception($"Error on setting value for block template with id: {blockTemplate.id}", e);
            //}

            var id = blockTemplate.id;
            var pool = CreatePool<BlockPool>(rootInstanceGO, $"Block pool: {id}", out var settings);
            pool.blockTemplate = blockTemplate;
            poolById[id] = pool;
            pool.Configure(settings);
        }
    }
}