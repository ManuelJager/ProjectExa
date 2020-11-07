using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;

namespace Exa.Grids.Blocks
{
    public class AliveBlockPoolGroup : BlockPoolGroupBase
    {
        protected override PrefabType PrefabType => PrefabType.alive;

        /// <summary>
        /// Creates an alive prefab on this group.
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateAlivePrefabGroup(BlockTemplate blockTemplate, BlockContext blockContext) {
            var rootInstanceGO = CreatePrefab(blockTemplate, PrefabType);
            var rootInstance = rootInstanceGO.GetComponent<Block>();

            var blockCollider = rootInstanceGO.GetComponentInChildren<BlockCollider>();
            blockCollider.Block = rootInstance;

            foreach (var component in rootInstance.GetBehaviours()) {
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