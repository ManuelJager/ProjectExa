using Exa.Bindings;
using Exa.Grids.Blocks.BlockTypes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public enum BlockPrefabType
    {
        inertGroup,
        defaultGroup,
        userGroup,
    }

    public class ObservableBlockTemplateCollection : ObservableCollection<ObservableBlockTemplate>
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

        public void StartUp()
        {
            foreach (var template in blockTemplateBag)
            {
                RegisterBlockTemplate(template);
            }
        }

        public GameObject GetInertBlock(string id, Transform transform)
        {
            return inertPrefabGroup.GetBlock(id, transform);
        }

        /// <summary>
        /// Get the block prefab with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Block GetBlock(string id, Transform transform, BlockPrefabType blockPrefabType)
        {
            return GetGroup(blockPrefabType)
                .GetBlock(id, transform)
                .GetComponent<Block>();
        }

        /// <summary>
        /// Register a template, and set the values on the block prefab
        /// </summary>
        /// <param name="blockTemplate"></param>
        private void RegisterBlockTemplate(BlockTemplate blockTemplate)
        {
            availibleBlockTemplates.Add(new ObservableBlockTemplate(blockTemplate));

            if (blockTemplatesDict.ContainsKey(blockTemplate.id))
            {
                throw new Exception("Duplicate block id found");
            }

            blockTemplatesDict[blockTemplate.id] = blockTemplate;

            inertPrefabGroup.CreateInertPrefab(blockTemplate);
            defaultPrefabGroup.CreateAlivePrefab(blockTemplate);
            userPrefabGroup.CreateAlivePrefab(blockTemplate);
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