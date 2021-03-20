using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Utils;
using UnityEngine;

namespace Exa.Research
{
    public class ResearchStore : MonoBehaviour
    {
        [SerializeField] private ResearchItemBag researchItemBag;

        private Dictionary<BlockContext, ResearchStepGroup> researchContexts;

        public void Init() {
            researchContexts = new Dictionary<BlockContext, ResearchStepGroup>();
            foreach (var blockContext in BlockContextExtensions.GetContexts()) {
                researchContexts.Add(blockContext, new ResearchStepGroup());
            }
        }

        public void AutoEnableItems() {
            foreach (var researchItem in researchItemBag) {
                researchItem.AutoEnable();
            }
        }

        public ResearchItem Find(string id) {
            return researchItemBag.FirstOrDefault(item => item.Id == id);
        }

        public T Find<T>()
            where T : ResearchItem {
            return researchItemBag.FindFirst<T>();
        }

        public T ApplyModifiers<T>(BlockContext context, T baseValues)
            where T : struct, IBlockComponentValues {
            return researchContexts[context].ApplyContext(baseValues);
        }

        public void AddModifier(BlockContext filter, IBlockComponentModifier modifier) {
            var steps = new List<ResearchStep>(modifier.GetResearchSteps());
            foreach (var (context, group) in FilterDict(filter)) {
                Systems.Blocks.Values.SetDirty(context, modifier);
                group.AddSteps(modifier, steps);
            }
        }

        public Action AddDynamicModifier<T>(
            ResearchStep<T>.ApplyValues applyFunc, 
            BlockContext filter = BlockContext.UserGroup, 
            ValueModificationOrder order = ValueModificationOrder.Multiplicative)
            where T : struct, IBlockComponentValues {

            var step = new ResearchStep<T>(applyFunc, order);
            var dynamicModifier = new DynamicBlockComponentModifier(step, template => {
                return template.GetTemplatePartials().Any(partial => typeof(T).IsAssignableFrom(partial.GetTargetType()));
            });
            
            AddModifier(filter, dynamicModifier);
            return () => RemoveModifier(filter, dynamicModifier);
        }

        public void RemoveModifier(BlockContext filter, IBlockComponentModifier modifier) {
            foreach (var (context, group) in FilterDict(filter)) {
                Systems.Blocks.Values.SetDirty(context, modifier);
                group.RemoveSteps(modifier);
            }
        }

        private IEnumerable<(BlockContext, ResearchStepGroup)> FilterDict(BlockContext filter) {
            foreach (var (key, context) in researchContexts.Unpack()) {
                if (filter.HasValue(key)) {
                    yield return (key, context);
                }
            }
        }
    }
}