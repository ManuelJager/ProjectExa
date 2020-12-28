using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using Exa.Types;
using Exa.Utils;
using UnityEngine;

namespace Exa.Research
{
    public class ResearchContext
    {
        public StepCache stepCache = new StepCache();

        public void AddSteps(string id, List<ResearchStep> cache) {
            stepCache.Add(id, cache);
        }

        public void RemoveSteps(string id) {
            stepCache.Remove(id);
        }

        public T ApplyContext<T>(T baseValues)
            where T : struct, IBlockComponentValues {
            var currentValues = baseValues;
            foreach (var (order, researchSteps) in GroupByStepAndFilter(currentValues)) {
                var initialValues = currentValues;

                T Reducer(T current, ResearchStep step) {
                    return (T) step.CalculateCurrentValues(initialValues, current);
                }

                currentValues = researchSteps.Aggregate(currentValues, Reducer);
            }
            return currentValues;
        }

        public IEnumerable<(ValueModificationOrder, IEnumerable<ResearchStep>)> GroupByStepAndFilter(IBlockComponentValues values) {
            List<ResearchStep> Reducer(List<ResearchStep> current, List<ResearchStep> steps) {
                current.AddRange(steps.Where(step => step.MatchesType(values)));
                return current;
            }

            return stepCache.Values.Aggregate(new List<ResearchStep>(), Reducer)
                .GroupBy(step => step.Order)
                .Select(grouping => (grouping.Key, (IEnumerable<ResearchStep>) grouping));
        }
    }

    public class StepCache : Dictionary<string, List<ResearchStep>>
    {

    }
}