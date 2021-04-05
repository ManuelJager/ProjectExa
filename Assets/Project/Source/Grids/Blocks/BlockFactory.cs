using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Exa.Types.Binding;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks
{
    public class ObservableBlockTemplateCollection : ObservableCollection<BlockTemplateContainer>
    { }

    /// <summary>
    /// Registers block types and creates block pools
    /// </summary>
    public class BlockFactory : MonoBehaviour
    {
        public ObservableBlockTemplateCollection blockTemplates = new ObservableBlockTemplateCollection();
        public Dictionary<string, BlockTemplate> blockTemplatesDict = new Dictionary<string, BlockTemplate>();

        [SerializeField] private BlockTemplateBag blockTemplateBag;
        [SerializeField] private InertBlockPoolGroup inertPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup defaultPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup userPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup enemyPrefabGroup;

        public BlockValuesStore Values { get; private set; }

        public T FindTemplate<T>()
            where T : class {
            return blockTemplateBag.FindFirst<T>();
        }

        public IEnumerator Init(IProgress<float> progress) {
            Values = new BlockValuesStore();
            var enumerator = EnumeratorUtils.ReportForeachOperation(blockTemplateBag, RegisterBlockTemplate, progress);
            while (enumerator.MoveNext()) yield return enumerator.Current;
        }

        public GameObject GetInactiveInertBlock(string id, Transform transform) {
            return inertPrefabGroup.GetInactiveBlock(id, transform);
        }

        /// <summary>
        /// Get the block prefab with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Block GetInactiveBlock(string id, Transform transform, BlockContext blockContext) {
            return GetGroup(blockContext)
                .GetInactiveBlock(id, transform)
                .GetComponent<Block>();
        }

        /// <summary>
        /// Register a template, and set the values on the block prefab
        /// </summary>
        /// <param name="blockTemplate"></param>
        private IEnumerator RegisterBlockTemplate(BlockTemplate blockTemplate) {
            if (blockTemplatesDict.ContainsKey(blockTemplate.id)) {
                throw new Exception("Duplicate block id found");
            }

            blockTemplates.Add(new BlockTemplateContainer(blockTemplate));
            blockTemplatesDict[blockTemplate.id] = blockTemplate;

            inertPrefabGroup.CreateInertPrefab(blockTemplate);
            yield return new WorkUnit();

            foreach (var context in BlockContextExtensions.GetContexts()) {
                Values.Register(context, blockTemplate);
                GetGroup(context).CreateAlivePrefabGroup(blockTemplate, context);
                yield return new WorkUnit();
            }
        }

        private AliveBlockPoolGroup GetGroup(BlockContext blockContext) {
            return blockContext switch {
                BlockContext.DefaultGroup => defaultPrefabGroup,
                BlockContext.UserGroup => userPrefabGroup,
                BlockContext.EnemyGroup => enemyPrefabGroup,
                _ => null
            };
        }
    }
}