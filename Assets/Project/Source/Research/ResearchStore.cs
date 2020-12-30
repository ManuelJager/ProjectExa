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
            return researchItemBag.FirstOrDefault(item => item is T) as T;
        }

        public T ApplyModifiers<T>(BlockContext context, T baseValues)
            where T : struct, IBlockComponentValues {
            return researchContexts[context].ApplyContext(baseValues);
        }

        public void AddModifier(BlockContext filter, BlockComponentModifier modifier) {
            var steps = new List<ResearchStep>(modifier.GetBaseSteps());
            foreach (var (context, group) in FilterDict(filter)) {
                Systems.Blocks.Values.SetDirty(context, modifier);
                group.AddSteps(modifier, steps);
            }
        }

        public void RemoveModifier(BlockContext filter, BlockComponentModifier modifier) {
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