using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using System;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System.Linq;

namespace Exa.Grids.Blocks
{
    public class BlockValuesStore
    {
        private Dictionary<BlockContext, BundleDictionary> contextDict;

        public BlockValuesStore()
        {
            contextDict = new Dictionary<BlockContext, BundleDictionary>();
        }

        public void Register(BlockContext blockContext, BlockTemplate blockTemplate)
        {
            var templateDict = EnsureCreated(blockContext);
            var id = blockTemplate.id;

            if (templateDict.ContainsKey(id))
            {
                throw new ArgumentException("Block template with given Id is already registered");
            }

            var bundle = new TemplateBundle
            {
                template = blockTemplate,
                valuesCache = GetValues(blockContext, blockTemplate),
                valuesAreDirty = false
            };

            templateDict.Add(id, bundle);
        }

        public void SetDirty(BlockContext blockContext, string id)
        {
            var bundle = contextDict[blockContext][id];
            bundle.valuesAreDirty = true;
            bundle.tooltip.IsDirty = true;
        }

        public Tooltip GetTooltip(BlockContext blockContext, string id)
        {
            return contextDict[blockContext][id].tooltip;
        }

        public void SetValues(BlockContext blockContext, string id, Block block)
        {
            var templateDict = contextDict[blockContext];
            var bundle = templateDict[id];

            if (bundle.valuesAreDirty)
            {
                bundle.valuesCache = GetValues(blockContext, bundle.template);
                bundle.valuesAreDirty = false;
            }

            bundle.valuesCache.ApplyValues(block);
        }

        private TemplateValuesCache GetValues(BlockContext blockContext, BlockTemplate template)
        {
            var dict = new TemplateValuesCache();

            foreach (var partial in template.GetTemplatePartials())
            {
                try
                {
                    var data = partial.GetValues(blockContext);
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
            public bool valuesAreDirty;
            public Tooltip tooltip;

            public TemplateBundle()
            {
                tooltip = new Tooltip(SelectTooltipComponents);
            }

            private IEnumerable<ITooltipComponent> SelectTooltipComponents()
            {
                var components = new List<ITooltipComponent>
                {
                    new TooltipTitle(template.displayId)
                };

                foreach (var componentData in valuesCache.Values)
                {
                    components.Add(new TooltipSpacer());
                    components.AddRange(componentData.GetTooltipComponents());
                }

                return components;
            }
        }
    }
}