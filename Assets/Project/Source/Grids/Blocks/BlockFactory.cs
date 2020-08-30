using Exa.Bindings;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Enum used to identify to which group a block belongs
    /// </summary>
    [Flags]
    public enum ShipContext : uint
    {
        None = 0,
        DefaultGroup = 1 << 0,
        UserGroup = 1 << 1,
        EnemyGroup = 1 << 2,
    }

    public class ObservableBlockTemplateCollection : ObservableCollection<BlockTemplateContainer>
    {
    }

    /// <summary>
    /// Registers block types and creates block pools
    /// </summary>
    public class BlockFactory : MonoBehaviour
    {
        public ObservableBlockTemplateCollection availibleBlockTemplates = new ObservableBlockTemplateCollection();
        public Dictionary<string, BlockTemplate> blockTemplatesDict = new Dictionary<string, BlockTemplate>();

        [SerializeField] private BlockTemplateBag blockTemplateBag;
        [SerializeField] private InertBlockPoolGroup inertPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup defaultPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup userPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup enemyPrefabGroup;

        public BlockValuesStore valuesStore;

        public IEnumerator StartUp(IProgress<float> progress)
        {
            valuesStore = new BlockValuesStore();
            var enumerator = EnumeratorUtils.ReportForeachOperation(blockTemplateBag, RegisterBlockTemplate, progress);
            while (enumerator.MoveNext()) yield return enumerator.Current;
        }

        public GameObject GetInactiveInertBlock(string id, Transform transform)
        {
            return inertPrefabGroup.GetInactiveBlock(id, transform);
        }

        /// <summary>
        /// Get the block prefab with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Block GetInactiveBlock(string id, Transform transform, ShipContext blockContext)
        {
            return GetGroup(blockContext)
                .GetInactiveBlock(id, transform)
                .GetComponent<Block>();
        }

        /// <summary>
        /// Register a template, and set the values on the block prefab
        /// </summary>
        /// <param name="blockTemplate"></param>
        private IEnumerator RegisterBlockTemplate(BlockTemplate blockTemplate)
        {
            availibleBlockTemplates.Add(new BlockTemplateContainer(blockTemplate));

            if (blockTemplatesDict.ContainsKey(blockTemplate.id))
            {
                throw new Exception("Duplicate block id found");
            }

            valuesStore.Register(ShipContext.DefaultGroup, blockTemplate);
            valuesStore.Register(ShipContext.UserGroup, blockTemplate);
            valuesStore.Register(ShipContext.EnemyGroup, blockTemplate);

            blockTemplatesDict[blockTemplate.id] = blockTemplate;

            inertPrefabGroup.CreateInertPrefab(blockTemplate);
            yield return null;
            defaultPrefabGroup.CreateAlivePrefabGroup(blockTemplate, ShipContext.DefaultGroup);
            yield return null;
            userPrefabGroup.CreateAlivePrefabGroup(blockTemplate, ShipContext.UserGroup);
            yield return null;
            enemyPrefabGroup.CreateAlivePrefabGroup(blockTemplate, ShipContext.EnemyGroup);
            yield return null;
        }

        private AliveBlockPoolGroup GetGroup(ShipContext blockContext)
        {
            switch (blockContext)
            {
                case ShipContext n when n.Is(ShipContext.DefaultGroup):
                    return defaultPrefabGroup;

                case ShipContext n when n.Is(ShipContext.UserGroup):
                    return userPrefabGroup;

                case ShipContext n when n.Is(ShipContext.EnemyGroup):
                    return enemyPrefabGroup;

                default:
                    return null;
            }
        }
    }
}