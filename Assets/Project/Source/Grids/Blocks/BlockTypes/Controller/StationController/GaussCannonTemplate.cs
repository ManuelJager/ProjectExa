using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/GaussCannon")]
    public class GaussCannonTemplate : StationControllerTemplate<GaussCannon> {
        [SerializeField] private GaussCannonTemplatePartial gaussCannonTemplatePartial;

        public override ITurretValues GetTurretValues(BlockContext context) {
            return gaussCannonTemplatePartial.ToContextfulComponentValues(context);
        }

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials()
                .Append(gaussCannonTemplatePartial);
        }
    }
}