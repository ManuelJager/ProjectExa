using Exa.Bindings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public class ObservableBlockTemplateCollection : ObservableCollection<ObservableBlockTemplate>
    {
    }

    public class BlockFactory : MonoBehaviour
    {
        [SerializeField] private BlockTemplateCollection blockTemplates;
        public ObservableBlockTemplateCollection availibleBlockTemplates = new ObservableBlockTemplateCollection();
        public Dictionary<string, BlockTemplate> blockTemplatesDict = new Dictionary<string, BlockTemplate>();

        public void OnEnable()
        {
            RegisterBlockTemplates();
        }

        public void RegisterBlockTemplates()
        {
            foreach (var blockTemplate in blockTemplates.blocks)
            {
                RegisterBlockTemplate(blockTemplate);
            }
        }

        private void RegisterBlockTemplate(BlockTemplate blockTemplate)
        {
            availibleBlockTemplates.Add(new ObservableBlockTemplate(blockTemplate));

            var block = blockTemplate.Prefab.GetComponent<IBlock>();

            blockTemplate.SetValues(block);

            var id = blockTemplate.Id;

            if (blockTemplatesDict.ContainsKey(id))
            {
                throw new Exception("Duplicate block id found");
            }

            blockTemplatesDict[id] = blockTemplate;
        }

        public GameObject GetBlock(string id)
        {
            return blockTemplatesDict[id].Prefab;
        }

        public BlockTemplate GetTemplate(string id)
        {
            return blockTemplatesDict[id];
        }
    }
}