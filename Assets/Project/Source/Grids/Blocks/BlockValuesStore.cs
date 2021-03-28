using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using System;
using System.Linq;
using Exa.Grids.Blocks.Components;
using Exa.Research;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks
{
    public class BlockValuesStore
    {
        private readonly Dictionary<BlockContext, BundleDictionary> contextDict;

        public BlockValuesStore() {
            contextDict = new Dictionary<BlockContext, BundleDictionary>();
        }

        public void Register(BlockContext blockContext, BlockTemplate blockTemplate) {
            var templateDict = EnsureCreated(blockContext);

            if (templateDict.ContainsKey(blockTemplate)) {
                throw new ArgumentException("Block template with given Id is already registered");
            }

            // Create a bundle with an empty cache, as research modifiers may yet still be applied
            var bundle = new TemplateBundle {
                template = blockTemplate,
                valuesCache = null,
                valuesAreDirty = true
            };

            templateDict.Add(blockTemplate, bundle);
        }

        public void SetDirty(BlockContext blockContext, IBlockComponentModifier modifier) {
            foreach (var dirtyBundle in FindBundles(blockContext, modifier)) {
                dirtyBundle.valuesAreDirty = true;
                dirtyBundle.tooltip.ShouldRefresh = true;
            }
        }

        public Tooltip GetTooltip(BlockContext blockContext, BlockTemplate blockTemplate) {
            return GetUpdatedBundle(blockContext, blockTemplate).tooltip;
        }

        public T GetValues<T>(BlockContext blockContext, BlockTemplate blockTemplate) 
            where T : struct, IBlockComponentValues {
            var bundle = GetUpdatedBundle(blockContext, blockTemplate);

            foreach (var value in bundle.valuesCache.Values) {
                if (value is T convertedValue) {
                    return convertedValue;
                }
            }

            throw new KeyNotFoundException($"Values of type {typeof(T)} was not found on block {blockTemplate}");
        }

        public bool TryGetValues<T>(BlockContext blockContext, BlockTemplate blockTemplate, out T output)
            where T : struct, IBlockComponentValues {
            var bundle = GetUpdatedBundle(blockContext, blockTemplate);

            foreach (var value in bundle.valuesCache.Values) {
                if (value.GetType() == typeof(T)) {
                    output = (T) value;
                    return true;
                }
            }

            output = default;
            return true;
        }

        public void SetValues(BlockContext blockContext, BlockTemplate blockTemplate, Block block) {
            var bundle = GetUpdatedBundle(blockContext, blockTemplate);
            bundle.valuesCache.ApplyValues(block);
        }

        private TemplateBundle GetUpdatedBundle(BlockContext blockContext, BlockTemplate blockTemplate) {
            var bundle = contextDict[blockContext][blockTemplate];

            if (bundle.valuesAreDirty) {
                bundle.valuesCache = ComputeValues(blockContext, bundle.template);
                bundle.valuesAreDirty = false;
            }

            return bundle;
        }

        private TemplateValuesCache ComputeValues(BlockContext blockContext, BlockTemplate template) {
            var dict = new TemplateValuesCache();

            foreach (var partial in template.GetTemplatePartials()) {
                try {
                    var data = partial.GetContextfulValues(blockContext);
                    dict.Add(partial, data);
                }
                catch (Exception e) {
                    throw new Exception($"Exception while setting values of {partial.GetType().Name} partial", e);
                }
            }

            return dict;
        }

        private BundleDictionary EnsureCreated(BlockContext blockContext) {
            if (!contextDict.ContainsKey(blockContext)) {
                contextDict.Add(blockContext, new BundleDictionary());
            }

            return contextDict[blockContext];
        }

        private IEnumerable<TemplateBundle> FindBundles(BlockContext context, IBlockComponentModifier modifier) {
            bool Filter(TemplateBundle bundle) {
                return modifier.AffectsTemplate(bundle.template);
            }

            return contextDict[context].Values.Where(Filter);
        }

        private class BundleDictionary : Dictionary<BlockTemplate, TemplateBundle>
        { }

        private class TemplateValuesCache : Dictionary<TemplatePartialBase, IBlockComponentValues>
        {
            public void ApplyValues(Block block) {
                foreach (var kvp in this) {
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
            public readonly Tooltip tooltip;

            public TemplateBundle() {
                tooltip = new Tooltip(GetTooltipGroup);
            }

            private TooltipGroup GetTooltipGroup() => new TooltipGroup(SelectTooltipComponents());

            private IEnumerable<ITooltipComponent> SelectTooltipComponents() {
                var components = new List<ITooltipComponent> {
                    new TooltipTitle(template.displayId), 
                    template.metadata
                };

                foreach (var componentData in valuesCache.Values) {
                    components.Add(new TooltipSpacer());
                    components.Add(new TooltipGroup(componentData.GetTooltipComponents(), 0, 8));
                }

                return components;
            }
        }
    }
}