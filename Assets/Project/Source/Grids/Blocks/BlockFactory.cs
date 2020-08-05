using Exa.Bindings;
using Exa.Grids.Blocks.BlockTypes;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exa.Utils;

namespace Exa.Grids.Blocks
{
    public enum BlockPrefabType
    {
        defaultGroup,
        userGroup,
    }

    public class ObservableBlockTemplateCollection : ObservableCollection<BlockTemplateContainer>
    {
    }

    /// <summary>
    /// Registers block types and sets default values
    /// </summary>
    public class BlockFactory : MonoBehaviour
    {
        public ObservableBlockTemplateCollection availibleBlockTemplates = new ObservableBlockTemplateCollection();
        public Dictionary<string, BlockTemplate> blockTemplatesDict = new Dictionary<string, BlockTemplate>();

        [SerializeField] private BlockTemplateBag blockTemplateBag;
        [SerializeField] private InertBlockFactoryPrefabGroup inertPrefabGroup;
        [SerializeField] private BlockFactoryPrefabGroup defaultPrefabGroup;
        [SerializeField] private BlockFactoryPrefabGroup userPrefabGroup;

        public IEnumerator StartUp(IProgress<float> progress)
        {
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
        public Block GetInactiveBlock(string id, Transform transform, BlockPrefabType blockPrefabType)
        {
            return GetGroup(blockPrefabType)
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

            blockTemplatesDict[blockTemplate.id] = blockTemplate;

            inertPrefabGroup.CreateInertPrefab(blockTemplate);
            yield return null;
            defaultPrefabGroup.CreateAlivePrefabGroup(blockTemplate);
            yield return null;
            userPrefabGroup.CreateAlivePrefabGroup(blockTemplate);
            yield return null;
        }

        private BlockFactoryPrefabGroup GetGroup(BlockPrefabType blockPrefabType)
        {
            switch (blockPrefabType)
            {
                case BlockPrefabType.defaultGroup:
                    return defaultPrefabGroup;

                case BlockPrefabType.userGroup:
                    return userPrefabGroup;

                default:
                    return null;
            }
        }
    }
}