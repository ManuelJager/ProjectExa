using System.Collections.Generic;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Research
{
    [CreateAssetMenu(menuName = "Research/GaussCannonExplosiveUpgrade")]
    public class GaussCannonExplosiveUpgrade : BlockComponentModifier<GaussCannonData>
    {
        public override IEnumerable<ResearchStep<GaussCannonData>> GetModifiers() {
            return new[] {
                new ResearchStep<GaussCannonData>(AdditiveStep, ValueModificationOrder.Addition)
            };
        }

        private void AdditiveStep(GaussCannonData initialData, ref GaussCannonData currentData) {
            currentData.damage += 20;
        }
    }
}