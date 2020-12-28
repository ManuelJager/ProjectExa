using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Research
{
    public class ResearchStore : MonoBehaviour
    {
        [SerializeField] private ResearchItemBag researchItemBag;

        private Dictionary<BlockContext, ResearchContext> researchContexts;

        private void Awake() {
            researchContexts = new Dictionary<BlockContext, ResearchContext>();
        }

        public void Init() {
            foreach (var context in BlockContextExtensions.GetContexts()) {
                AddContext(context);
            }

            Find<GaussCannonExplosiveUpgrade>().AddSelf(BlockContext.UserGroup);
        }

        public T Find<T>()
            where T : class {
            return researchItemBag.FirstOrDefault(item => item is T) as T;
        }

        public void AddContext(BlockContext blockContext) {
            researchContexts.Add(blockContext, new ResearchContext());
        }

        public ResearchContext GetContext(BlockContext blockContext) {
            return researchContexts[blockContext];
        }
    }
}