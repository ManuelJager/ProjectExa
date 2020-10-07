using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Autocannon")]
    public class AutocannonTemplate : BlockTemplate<Autocannon>
    {
        [SerializeField] private AutocannonTemplatePartial _autocannonTemplatePartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials()
        {
            return base.GetTemplatePartials()
                .Append(_autocannonTemplatePartial);
        }
    }
}