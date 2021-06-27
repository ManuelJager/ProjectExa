using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Autocannon")]
    public class AutocannonTemplate : BlockTemplate<Autocannon>, ITurretTemplate {
        [SerializeField] private AutocannonTemplatePartial autocannonTemplatePartial;

        public ITurretValues GetTurretValues(BlockContext context) {
            return autocannonTemplatePartial.ToContextfulComponentValues(context);
        }

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials()
                .Append(autocannonTemplatePartial);
        }
    }
}