using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using System;
using Exa.Grids.Blocks.Components;
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

            var bundle = new TemplateBundle {
                template = blockTemplate,
                valuesCache = GetValues(blockContext, blockTemplate),
                valuesAreDirty = false
            };

            templateDict.Add(blockTemplate, bundle);
        }

        public void SetDirty(BlockContext blockContext, BlockTemplate blockTemplate) {
            var bundle = contextDict[blockContext][blockTemplate];
            bundle.valuesAreDirty = true;
            bundle.tooltip.ShouldRefresh = true;
        }

        public Tooltip GetTooltip(BlockContext blockContext, BlockTemplate blockTemplate) {
            return contextDict[blockContext][blockTemplate].tooltip;
        }

        public bool TryGetValues<T>(BlockContext blockContext, BlockTemplate blockTemplate, out T output)
            where T : struct, IBlockComponentValues {
            var bundle = contextDict[blockContext][blockTemplate];

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
            var bundle = contextDict[blockContext][blockTemplate];

            if (bundle.valuesAreDirty) {
                bundle.valuesCache = GetValues(blockContext, bundle.template);
                bundle.valuesAreDirty = false;
            }

            bundle.valuesCache.ApplyValues(block);
        }

        private TemplateValuesCache GetValues(BlockContext blockContext, BlockTemplate template) {
            var dict = new TemplateValuesCache();

            foreach (var partial in template.GetTemplatePartials()) {
                try {
                    var data = partial.GetValues(blockContext);
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
                    new TooltipTitle(template.displayId)
                };

                foreach (var componentData in valuesCache.Values) {
                    components.Add(new TooltipSpacer());
                    components.AddRange(componentData.GetTooltipComponents());
                }

                return components;
            }
        }
    }
}