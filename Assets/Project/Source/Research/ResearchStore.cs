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
        public delegate void ResearchChangedDelegate(BlockContext blockContext);
        
        [SerializeField] private ResearchItemBag researchItemBag;

        private Dictionary<BlockContext, ResearchStepGroup> researchContexts;

        public event ResearchChangedDelegate ResearchChanged;

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
                group.AddSteps(modifier, steps);
                Systems.Blocks.Values.SetDirty(context, modifier);
                ResearchChanged?.Invoke(context);
            }
        }

        public Action AddDynamicModifier<T>(
            BlockContext filter,
            ResearchStep<T>.ApplyValues applyFunc,
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
                group.RemoveSteps(modifier);
                Systems.Blocks.Values.SetDirty(context, modifier);
                ResearchChanged?.Invoke(context);
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