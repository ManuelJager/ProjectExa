using Exa.Grids.Blocks.BlockTypes;

namespace Exa.Grids.Blocks
{
    public class AliveBlockPoolGroup : BlockPoolGroupBase
    {
        protected override PrefabType PrefabType => PrefabType.Alive;

        /// <summary>
        /// Creates an alive prefab on this group.
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateAlivePrefabGroup(BlockTemplate blockTemplate, ShipContext blockContext)
        {
            var rootInstanceGo = CreatePrefab(blockTemplate, PrefabType);
            var rootInstance = rootInstanceGo.GetComponent<Block>();

            foreach (var component in rootInstance.GetBehaviours())
            {
                component.block = rootInstance;
            }

            var id = blockTemplate.id;
            var pool = CreatePool<BlockPool>(rootInstanceGo, $"Block pool: {id}", out var settings);
            pool.blockTemplate = blockTemplate;
            pool.blockContext = blockContext;
            poolById[id] = pool;
            pool.Configure(settings);
        }
    }
}