using Exa.Bindings;
using Exa.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
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
        
        public void StartUp()
        {
            foreach (var template in blockTemplateBag)
            {
                RegisterBlockTemplate(template);
            }
        }

        /// <summary>
        /// Register a template, and set the values on the block prefab
        /// </summary>
        /// <param name="blockTemplate"></param>
        private void RegisterBlockTemplate(BlockTemplate blockTemplate)
        {
            availibleBlockTemplates.Add(new ObservableBlockTemplate(blockTemplate));

            var block = blockTemplate.prefab.GetComponent<IBlock>();

            try
            {
                blockTemplate.SetValues(block);
            }
            catch (Exception e)
            {
                throw new Exception($"Error on setting value for block template with id: {blockTemplate.id}", e);
            }

            if (blockTemplatesDict.ContainsKey(blockTemplate.id))
            {
                throw new Exception("Duplicate block id found");
            }

            blockTemplatesDict[blockTemplate.id] = blockTemplate;
        }

        /// <summary>
        /// Get the block prefab with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameObject GetBlock(string id)
        {
            return blockTemplatesDict[id].prefab;
        }

        /// <summary>
        /// Get block template
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlockTemplate GetTemplate(string id)
        {
            return blockTemplatesDict[id];
        }
    }
}