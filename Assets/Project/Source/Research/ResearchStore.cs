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

        private Dictionary<BlockContext, ResearchContext> researchContexts;

        public void Init() {
            researchContexts = new Dictionary<BlockContext, ResearchContext>();
            foreach (var blockContext in BlockContextExtensions.GetContexts()) {
                researchContexts.Add(blockContext, new ResearchContext());
            }
        }

        public T Find<T>()
            where T : class {
            return researchItemBag.FirstOrDefault(item => item is T) as T;
        }

        public T ApplyModifiers<T>(BlockContext context, T baseValues)
            where T : struct, IBlockComponentValues {
            return researchContexts[context].ApplyContext(baseValues);
        }

        public void AddModifier(BlockContext filter, BlockComponentModifier modifier) {
            var steps = new List<ResearchStep>(modifier.GetBaseSteps());
            foreach (var context in FilterDict(filter)) {
                context.AddSteps(modifier.Id, steps);
            }
        }

        public void RemoveModifier(BlockContext filter, BlockComponentModifier modifier) {
            foreach (var context in FilterDict(filter)) {
                context.RemoveSteps(modifier.Id);
            }
        }

        private IEnumerable<ResearchContext> FilterDict(BlockContext filter) {
            foreach (var (key, context) in researchContexts.Unpack()) {
                if (filter.HasValue(key)) {
                    yield return context;
                }
            }
        }
    }
}