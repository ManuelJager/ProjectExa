using Exa.Grids.Blocks.BlockTypes;

namespace Exa.Grids.Blocks
{
    public class AliveBlockPoolGroup : BlockPoolGroupBase
    {
        protected override BlockTemplatePrefabType PrefabType => BlockTemplatePrefabType.alive;

        /// <summary>
        /// Creates an alive prefab on this group.
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateAlivePrefabGroup(BlockTemplate blockTemplate, ShipContext blockContext)
        {
            var rootInstanceGO = CreatePrefab(blockTemplate, PrefabType);
            var rootInstance = rootInstanceGO.GetComponent<Block>();

            foreach (var component in rootInstance.GetBehaviours())
            {
                component.block = rootInstance;
            }

            var id = blockTemplate.id;
            var pool = CreatePool<BlockPool>(rootInstanceGO, $"Block pool: {id}", out var settings);
            pool.blockTemplate = blockTemplate;
            pool.blockContext = blockContext;
            poolById[id] = pool;
            pool.Configure(settings);
        }
    }
}