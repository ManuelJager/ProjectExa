using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Thruster")]
    public class ThrusterTemplate : BlockTemplate<Thruster>
    {
        [SerializeField] protected ThrusterTemplatePartial thrusterTemplatePartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials()
                .Append(thrusterTemplatePartial);
        }
    }
}