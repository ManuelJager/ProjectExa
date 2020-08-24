using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using System;
using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks
{
    public class BlockValuesStore
    {
        private Dictionary<BlockContext, BundleDictionary> contextDict;

        public BlockValuesStore()
        {
            contextDict = new Dictionary<BlockContext, BundleDictionary>();
        }

        public void Register(BlockContext blockContext, string id, BlockTemplate blockTemplate)
        {
            var templateDict = EnsureCreated(blockContext);
            
            if (templateDict.ContainsKey(id))
            {
                throw new ArgumentException("Block template with given Id is already registered");
            }

            var bundle = new TemplateBundle
            {
                template = blockTemplate,
                valuesCache = null,
                isDirty = true
            };

            templateDict.Add(id, bundle);
        }

        public void SetValues(BlockContext blockContext, string id, Block block)
        {
            var templateDict = contextDict[blockContext];
            var bundle = templateDict[id];

            if (bundle.isDirty)
            {
                bundle.valuesCache = CreateAndSetValues(blockContext, bundle.template, block);
                bundle.isDirty = false;
            }
            else
            {
                bundle.valuesCache.ApplyValues(block);
            }
        }

        private TemplateValuesCache CreateAndSetValues(BlockContext blockContext, BlockTemplate template, Block block)
        {
            var dict = new TemplateValuesCache();

            foreach (var partial in template.GetTemplatePartials())
            {
                try
                {
                    var data = partial.SetValues(block, blockContext);
                    dict.Add(partial, data);
                }
                catch (Exception e)
                {
                    throw new Exception($"Exception while setting values of {partial.GetType().Name} partial", e);
                }
            }

            return dict;
        }

        private BundleDictionary EnsureCreated(BlockContext blockContext)
        {
            if (!contextDict.ContainsKey(blockContext))
            {
                contextDict.Add(blockContext, new BundleDictionary());
            }

            return contextDict[blockContext];
        }

        private class BundleDictionary : Dictionary<string, TemplateBundle>
        {
        }

        private class TemplateValuesCache : Dictionary<TemplatePartialBase, IBlockComponentData>
        {
            public void ApplyValues(Block block)
            {
                foreach (var kvp in this)
                {
                    var templatePartial = kvp.Key;
                    var values = kvp.Value;
                    templatePartial.SetValues(block, values);
                }
            }
        }

        private class TemplateBundle
        {
            public BlockTemplate template;
            public TemplateValuesCache valuesCache;
            public bool isDirty;
        }
    }
}